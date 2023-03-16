import React, { useEffect, useState } from 'react';
import EditScreenInfo from '../components/EditScreenInfo';
import { TouchableOpacity } from "react-native-gesture-handler";
import { StyleSheet, ActivityIndicator, FlatList, Text, View, Platform, Button, Image, Dimensions, TextInput  } from 'react-native';
import { RootTabScreenProps } from '../types';
import CardMenu from '../components/CardMenu';
import { MaterialIcons, MaterialCommunityIcons } from '@expo/vector-icons';
import Slider from '@react-native-community/slider';
import Checkbox from 'expo-checkbox';


import * as api from '../constants/Api';

export default function TabTwoScreen({ navigation }) {
  const [isLoading, setLoading] = useState(true);
  const [isFetching, setIsFetching] = useState(false);
  const [showFilter, setShowFilter] = useState(false);
  const [plat, setPlat] = useState('');
  const [prix, setPrix] = useState(60);
  const [prixDisplay, setPrixDisplay] = useState(60);
  const [isChecked, setChecked] = useState(true);
  const [distance, setDistance] = useState(2000);
  const [distanceDisplay, setDistanceDisplay] = useState(2000);
  const [lieu, setLieu] = useState('');
  const [data, setData] = useState([]);

  //Flo > Utile pour recharger les datas mais s'enclenche trop souvent
  //navigation.addListener('focus', () => console.log('Screen is Focus'));

  const fetchData = () => {
    getMenus();
    setIsFetching(false);
  };
  
  const onRefresh = () => {
    setIsFetching(true);
    fetchData();
  };

  const getMenus = async () => {

    var filtre= 'Menu/filtres?';
    if(plat) {
     filtre = filtre + '&plat=' + plat;
     console.log(filtre);
    }
    if(prix) {
      filtre = filtre + '&prixmax=' + prix;
      console.log(filtre);
     }
     if(lieu) {
      filtre = filtre + '&city=' + lieu;
      console.log(filtre);
     }

    try {
      console.log(filtre);
      const response = await fetch('https://api.golun.ch/api/' + filtre);
      const json = await response.json();
      setData(json);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getMenus();
  }, []);

  const onPressFilter = () => {
    setShowFilter(!showFilter)
    setIsFetching(true);
    fetchData();
  };


  return (
    
    <View style={{ flex: 1, height: '100%', backgroundColor: '#FFF' }}>
       {/* <CardMenu/>  */}
      {isLoading ? (
        <ActivityIndicator />
      ) : (
        <>
        {showFilter && (
        <View style={{ justifyContent: 'flex-start', alignItems: 'flex-start', borderColor:"#79B788", borderBottomWidth:5, paddingBottom:20, paddingHorizontal: 20, paddingTop:10 }}>
          <Text>Rechercher un Plat :</Text>
          <TextInput
          style={styles.textInput}
          placeholder="Ex. Steak"
          onChangeText={setPlat}
          value={plat}
          />
          <Text>Prix maximum {prixDisplay} CHF</Text>
          <Slider
            style={{width: '100%', height: 30}}
            minimumValue={1}
            maximumValue={80}
            step={1}
            value={prix}
            onSlidingComplete={value => setPrix(value)}
            onValueChange={value => setPrixDisplay(value)}
            minimumTrackTintColor="#79B788"
            maximumTrackTintColor="#000000"
            thumbTintColor='#79B788'
          />
          <View style={{ flexDirection: "row", marginBottom:10 }}>
          <Checkbox
          style={styles.checkbox}
          value={isChecked}
          onValueChange={setChecked}
          color={isChecked ? '#79B788' : 'lightgray'}
        /> 
        <Text style={{ marginTop:5, marginHorizontal:5  }}>Utiliser ma Géolocalisation</Text></View>
        {isChecked && (
        <>
        <Text>Distance maximum {distanceDisplay} mètres</Text>
          <Slider
            style={{width: '100%', height: 31}}
            minimumValue={500}
            maximumValue={10000}
            step={500}
            value={distance}
            onSlidingComplete={value => setDistance(value)}
            onValueChange={value => setDistanceDisplay(value)}
            minimumTrackTintColor="#79B788"
            maximumTrackTintColor="#000000"
            thumbTintColor='#79B788'
          />
          </>  
        )}

        {!isChecked && (
        <>
        <TextInput
          style={styles.textInput}
          placeholder="Ville ex. Yverdon"
          onChangeText={setLieu}
          value={lieu}
          />
          </>  
        )}

        </View>
        )}
        <View style={{ justifyContent: 'space-around', alignItems: 'center', flexDirection: "row" }}>
        {!showFilter && (  
        <TouchableOpacity
            style={[styles.btn]}
            onPress={() => navigation.navigate('AddMenu')}>
            <View style={{ flexDirection: "row", alignItems: 'center', justifyContent: 'center' }}>
            {/* <MaterialIcons name="playlist-add" size={24} color="white" />  */}
            <Text style={[styles.textBtn,{color: '#ffffff'}]}>
            Ajouter un menu 
            </Text>
            </View>
            </TouchableOpacity>
            )}
            <TouchableOpacity
            style={[styles.btn]}
            onPress={onPressFilter}>
            <Text style={[styles.textBtn,{color: '#ffffff'}]}>
            {/* {showFilter ? 'Fermer' : 'Filtrer'}*/} Filtrer <MaterialCommunityIcons name="filter" size={16} color="white" /> 
            </Text>
            </TouchableOpacity>
        </View>
        
        <FlatList
          style={{ flex: 1, flexDirection: "column" }}
          contentContainerStyle={{alignItems:'flex-start'}}
          data={data}
          onRefresh={onRefresh}
          refreshing={isFetching}
          keyExtractor={({ id }, index) => id}
          renderItem={({ item }) => (

            <TouchableOpacity style={styles.card} activeOpacity={0.2} onPress={() => {
              navigation.navigate('MenuDetails', {data: item})}}>
        <View style={{ flexDirection: "row"}}>
          <View style={styles.cardImage}>
          {item.platPhoto ? (
          <Image
          // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
          style={{ width: 80, height: 80,  borderRadius: 10  }}
          source={{uri: item.platPhoto}}
          />
          ) : (
            <Image
            // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
            style={{ width: 80, height: 80,  borderRadius: 10  }}
            source={require('../assets/images/no_photo.jpg')}
          />
          )}
          </View>
          <View style={{ flex: 1 }}>
            <Text style={styles.cardTitle}>{item.plat}</Text>
            {/* <Text style={styles.cardLocation}>location</Text> */}
            {/* <Text style={styles.cardDescription}>{item.plat}</Text> */}
            <View style={{ flexDirection: "row"}}>
            <MaterialCommunityIcons name="leaf" size={25}  style={ item.entree ? styles.iconOn : styles.iconOff }/>
            <MaterialCommunityIcons name="cupcake" size={25}  style={ item.dessert ? styles.iconOn : styles.iconOff }/>
            <MaterialIcons name="local-drink" size={25} style={ item.inclusBoisson == true ? styles.iconOn : styles.iconOff}/>
            <MaterialCommunityIcons name="coffee" size={25}  style={item.inclusCafe == true ? styles.iconOn : styles.iconOff }/>
                </View>
            </View>
            <View style={{ flexDirection: "column" }}>
            <Text style={styles.cardPrice}>{item.prix} CHF</Text>
            </View>
          </View>
      </TouchableOpacity>
            
          )}
        />
        </>
      )}
    </View>
  );
};

const { width, height } = Dimensions.get("screen");

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFF'
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
  card: {
    marginVertical: 10,
    backgroundColor: "#fff",
    paddingVertical: 15,
    paddingHorizontal: 15,
    width: width / 1.1,
    marginHorizontal: 20,
    borderRadius: 20,
    height: height / 7.5 ,
    shadowColor: "#000",
    elevation: 5,
    shadowOffset: {
      width: 2,
      height: 2,
    },
    shadowOpacity: 0.3,
    shadowRadius: 1.5,
  },

  cardTitle: {
    fontWeight: "bold",
    fontSize: 14,
    marginLeft: 10,
  },

  cardLocation: {
    fontSize: 11.5,
    color: "#777",
    marginLeft: 10,
  },

  cardDescription: {
    fontSize: 12,
    marginVertical: 5,
    marginLeft: 10,
  },

  cardPrice: {
    fontWeight: "bold",
    fontSize: 13,
    borderWidth: 2, 
    borderRadius: 5,
    margin:5,
    padding: 5,
  },

  cardImage: {
   flex: 0.5,
  }, 

  iconOff:{
    color: "lightgray",
    marginLeft: 10,
  },
  iconOn:{
    color: "#79B788",
    marginLeft: 10,
  },
  btn: {
    width: '100%',
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 10,
    borderTopLeftRadius:0,
    borderTopRightRadius:0,
    borderColor: '#79B788',
    backgroundColor: '#79B788',
    borderWidth: 1,
  },
  textBtn: {
    fontSize: 16,
    fontWeight: 'bold',
    marginHorizontal:20
  },
  textInput: {
    borderColor: "lightgray",
    borderWidth: 1,
    borderRadius: 3,
    paddingLeft: 10,
    paddingHorizontal:10,
    paddingVertical:5,
    marginVertical:5,
    width:'100%'
  },
  checkbox: {
    marginVertical:5,
  },
});