import { Alert, Text, StyleSheet, View, Dimensions  } from 'react-native';
import React, { useEffect, useState, useRef } from 'react';
import * as Location from 'expo-location';
import MapView from 'react-native-maps';
import { Marker } from 'react-native-maps';
import * as api from '../constants/Api';
import { getDistance, isPointWithinRadius } from 'geolib';
import { AuthContext, AuthProvider } from '../context/auth';

export default function TabThreeScreen({ route, navigation }) {
  const { state, dispatch } = React.useContext(AuthContext);
  const [isLoading, setLoading] = useState(true);
  const [locationServiceEnabled, setLocationServiceEnabled] = useState(false);
  const [displayCurrentAddress, setDisplayCurrentAddress] = useState('Wait');
  const [location, setLocation] = useState(null);
  const [latitude, setLatitude] = useState(state.userLocation.latitude);
  const [longitude, setLongitude] = useState(state.userLocation.longitude);
  const [ghostlat, setGhostLat] = useState(state.userLocation.latitude);
  const [ghostlong, setGhostLong] = useState(state.userLocation.longitude);
  const [ghostdist, setGhostDist] = useState(null);
  const [ghoston, setGhostOn] = useState(null);
  const [data, setData] = useState([]);
  const [count, setCount] = useState(0);

  //Flo constantes Calcul du DELTA par rapport à la taille de la fenêtre sur mobile
  const { width, height } = Dimensions.get('window');
  const ASPECT_RATIO = width / height;
  const LATITUDE_DELTA = 0.0922;
  //const LATITUDE_DELTA = 0.0122;
  const LONGITUDE_DELTA = LATITUDE_DELTA * ASPECT_RATIO;  

  //Flo cet useEffect s'execute 1 fois grâce à []
  useEffect(() => {
    CheckIfLocationEnabled();
    PacManGhost(state.userLocation.latitude, state.userLocation.longitude);
    GetCurrentLocation();
    getPlaces();
  }, []);

  //Flo cet useEffect s'execute en permanence quand le state global change
  useEffect(() => {
    CalculateDistance();
    OnGhost();
  });

  const CheckIfLocationEnabled = async () => {
    let enabled = await Location.hasServicesEnabledAsync();

    if (!enabled) {
      Alert.alert(
        'Location Service not enabled',
        'Please enable your location services to continue',
        [{ text: 'OK' }],
        { cancelable: false }
      );
    } else {
      setLocationServiceEnabled(enabled);
    }
  };
  const UserCurrentLocation = () => {
  //async function UserCurrentLocation(glat, glong) {
    console.log('Get Geolocation from global state');

    setLatitude(state.userLocation.latitude);
    setLongitude(state.userLocation.longitude);   
    
  }

  //async function PacManGhost (lat, long) {
  const PacManGhost = (lat, long) => {
    var r = 80/111300 // = 80 meters
    , y0 = lat
    , x0 = long
    , u = Math.random()
    , v = Math.random()
    , w = r * Math.sqrt(u)
    , t = 2 * Math.PI * v
    , x = w * Math.cos(t)
    , y1 = w * Math.sin(t)
    , x1 = x / Math.cos(y0)

    setGhostLat(y0 + y1);
    setGhostLong(x0 + x1);

    var dis = getDistance(
      { latitude: state.userLocation.latitude, longitude: state.userLocation.longitude },
      { latitude: y0 + y1, longitude: x0 + x1 }
    );
    setGhostDist(dis);

    var on = isPointWithinRadius(
      { latitude: state.userLocation.latitude, longitude: state.userLocation.longitude },
      { latitude: y0 + y1, longitude: x0 + x1 },
      20 //20 metres du fantome pour gagner
    );

    setGhostOn(on)
    
  }

  const CalculateDistance = () => {
    var dis = getDistance(
      { latitude: state.userLocation.latitude, longitude: state.userLocation.longitude },
      { latitude: ghostlat, longitude: ghostlong }
    );
    setGhostDist(dis);
    //alert(`Distance\n\n${dis} metres\nOR\n${dis / 1000} KM`);
  };

  const OnGhost = () => {
    var eatghost = isPointWithinRadius(
      { latitude: state.userLocation.latitude, longitude: state.userLocation.longitude },
      { latitude: ghostlat, longitude: ghostlong },
      20
    );
    setGhostOn(eatghost)
    if(eatghost == true) {
      setCount(count + 1);

      if(count > 0) {
      alert(`Congrats 🏆 YOU WIN ${count} time(s) ! Try again:`);
      }
       
      PacManGhost(state.userLocation.latitude, state.userLocation.longitude);
    }
  }



  const GetCurrentLocation = async () => {
    //demande la permission d'utiliser le gps au premier plan test
    let { status } = await Location.requestForegroundPermissionsAsync();

    if (status !== 'granted') {
      Alert.alert(
        'Permission not granted',
        'Allow the app to use location service.',
        [{ text: 'OK' }],
        { cancelable: false }
      );
    }

    try {
      let latitude = state.userLocation.latitude;
      let longitude = state.userLocation.longitude;

        //Récupère l'adresse depuis les coordonnées
        let response = await Location.reverseGeocodeAsync({
          latitude,
          longitude,
        });
        console.log(response);
        //Récupère tous les champs qu on a besoin pour la DB
        for (let item of response) {
          let address = `${item.street} ${item.streetNumber}, ${item.postalCode}, ${item.city}, ${item.country}`;

          setDisplayCurrentAddress(address);
        }
      
    } catch (e) {
      alert('Bug transformation adresse');
      console.log('Bug: ', e);
    }
  };

  const getPlaces = async () => {
    try {
      const response = await fetch(api.GETPLACES);
      const json = await response.json();
      setData(json);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const mapMarkers = () => {
    return data.map((report) => <Marker
      key={report.id}
      coordinate={{ latitude: report.posLatitude, longitude: report.posLongitude }}
      title={report.ville}
      description={report.adresse}
      image={require('../assets/images/map-marker-red-dish.png')} 
      /* pinColor={'#79B788'} */
    >
    </Marker >)
  }

  return (
    <View style={styles.container}>
      <MapView style={styles.map} initialRegion={{
        //Lausanne Gare Lat, Long : 46.51722214068109, 6.629148603948812
        //latitude: 46.51722214068109,
        //longitude: 6.629148603948812,
        //Taff flo LAt, Long : 46.32173471889709, 6.9664981
        //latitude: 46.32173471889709,
        //longitude: 6.9664981,
        latitude: latitude,
        longitude: longitude,
        latitudeDelta: LATITUDE_DELTA,
        longitudeDelta: LONGITUDE_DELTA,
      }} >
      {mapMarkers()}
      {/* Flo ajout de la position de la personne  */}
      <Marker
        key={'x'}
        coordinate={{ latitude: state.userLocation.latitude, longitude: state.userLocation.longitude }}
        title={'Vous'}
        description={'Votre position'}
        image={require('../assets/images/map-pacman.png')} 
        /* pinColor={'#79B788'} */
        >
      </Marker >
      {/* Flo PacMan Ghost */}
      <Marker
        key={'g'}
        coordinate={{ latitude: Number(ghostlat), longitude: Number(ghostlong) }}
        title={'PacMan'}
        description={'Mangez tous les fantômes'}
        image={require('../assets/images/map-pacman-ghost.png')} 
        >
      </Marker > 
      </MapView>
      
      {/* <Text>{JSON.stringify(location)}</Text> */}
      <View style={styles.overlay}>
      <Text style={styles.title}>🏆 {count - 1}</Text>
      <Text>Latitude : {state.userLocation.latitude}</Text>
      <Text>Longitude : {state.userLocation.longitude}</Text>
      <Text>{displayCurrentAddress}</Text>
      <Text>{ghostlat} {ghostlong}</Text>
      <Text>{ghoston ? 'oui' : 'non'} - {ghostdist}</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  map: {
    width: Dimensions.get('window').width,
    height: Dimensions.get('window').height,
  },
  overlay: { 
    position: "absolute", 
    bottom: 50
  }
});
