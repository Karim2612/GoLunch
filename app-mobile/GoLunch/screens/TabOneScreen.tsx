import { StyleSheet, Button, ScrollView, TextInput, TouchableOpacity } from 'react-native';
import * as React from 'react';
import { Text, View } from '../components/Themed';
import * as SecureStore from 'expo-secure-store';
import { AuthContext, AuthProvider } from '../context/auth';
import CityCard from "../components/CityCard";
import * as Location from 'expo-location';

export default function TabOneScreen({ navigation }) {
  const { state, dispatch } = React.useContext(AuthContext);
  const [userCity, setUserCity] = React.useState('Wait');

  const authContext = React.useMemo(
    () => ({
      logOut: async () => {
        await SecureStore.deleteItemAsync('userToken');
        dispatch({ type: 'LOG_OUT' });
      },
      clearErrorMessage: () => dispatch({ type: 'CLEAR_AUTH_ERROR' }),
    }),
    []
  );

  React.useEffect(() => {
    getUserCity();
  }, []);

  const getUserCity = async () => {
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
          let address = `${item.city}`;

          setUserCity(address);
        }
      
    } catch (e) {
      alert('Bug transformation adresse');
      console.log('Bug: ', e);
    }
  };



  return (
    <View style={styles.container}>

<View style={{ flex: 1, paddingTop: 5 }}>
            <Text style={{ fontSize: 26, paddingHorizontal: 15, fontFamily: 'DonaAlt-Medium'}}>
              Où souhaitez-vous Dîner ?
              </Text>

          <View style={{ height: 130, marginTop: 20 }}>
            <ScrollView
              horizontal={true}
              showsHorizontalScrollIndicator={false}
            >
              <CityCard
                cityImg={require("../assets/cities/city-lausanne.jpg")}
                cityName="Lausanne"
              />
              <CityCard
                cityImg={require("../assets/cities/city-montreux.jpg")}
                cityName="Montreux"
              />
              <CityCard
                cityImg={require("../assets/cities/city-vevey.jpg")}
                cityName="Vevey"
              />
              <CityCard
                cityImg={require("../assets/cities/city-aigle.jpg")}
                cityName="Aigle"
              />
              <CityCard
                cityImg={require("../assets/cities/city-geneve.jpg")}
                cityName="Genève"
              />
              
            </ScrollView>
          </View>

          <View style={{ marginTop: 20 }}>
            <TextInput
              style={{
                height: 50,
                borderColor: "lightgray",
                borderWidth: 1,
                borderRadius: 5,
                marginHorizontal: 20,
                paddingLeft: 10
              }}
              placeholder="Ex. Aigle"
            />
            <Text
              style={{
                paddingHorizontal: 15,
                paddingTop: 5,
                textAlign: "center",
                color: "lightgrey"
              }}
            >
              Choisissez une localité ou entrez un nom de Ville
              </Text>
          </View>

          <View style={{ paddingHorizontal: 15, paddingTop: 5 }}>
          <TouchableOpacity
            style={[
              styles.btn,
              {
                borderColor: '#79B788',
                backgroundColor: '#79B788',
                borderWidth: 1,
                marginTop: 15,
              },
            ]}
            
            onPress={() => {
              
            }}
          >
            <Text
              style={[
                styles.textBtn,
                {
                  color: '#ffffff',
                },
              ]}
            >
              Chercher
            </Text>
            </TouchableOpacity>
          </View>

          <Text style={{ fontSize: 26, paddingHorizontal: 15, paddingTop: 20, fontFamily: 'DonaAlt-Medium'}}>
              Menus à {userCity}
              </Text>

          </View>    
      <View style={{ paddingTop: 5, justifyContent: 'center', alignItems: 'center' }}>
      {state.userToken == null ? (
        <Text>Logged out 🔓</Text>
      ) : (
        <View>
          <Text>Logged in 🔒</Text>
          <Button onPress={authContext.logOut} color='#79B788' title='Logout' />
        </View>
      )}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
    fontFamily: 'DonaAlt-Medium'
  },
  btn: {
    width: '100%',
    height: 45,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 10,
  },
  textBtn: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
});
