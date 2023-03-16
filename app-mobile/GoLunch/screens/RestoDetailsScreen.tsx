import React, { useEffect, useState } from 'react';
import { Button, Image, View, Text, TouchableOpacity, StyleSheet, Alert ,  ActivityIndicator,FlatList} from 'react-native'
import { AntDesign } from '@expo/vector-icons'; 
import { MaterialIcons, MaterialCommunityIcons, Ionicons } from '@expo/vector-icons';

import * as api from '../constants/Api';

export default function RestoDetailsScreen({ route, navigation }) { 
    const { data } = route.params;
    const [isFavorite, checkFavorite] = useState(false);
    const [idFavorite, checkIdFavorite] = useState(false)

  const createTwoButtonAlert = () =>
    Alert.alert('Info de contact ', '', [
      {
        text: 'Fermer',
        onPress: () => console.log('Cancel Pressed'),
        style: 'cancel',
      },
      { text: 'OK', onPress: () => console.log('OK Pressed') },
    ]);

  const onScreenLoad = async () => {
    try {
      const response = await fetch('https://api.golun.ch/api/fav/get/' + data.id + '/1');
      if (!response.ok) {
        console.log('Pas favoris');
      } else {
          console.log('Est favoris');
          const json = await response.json();
          if(json.id != null) {
            checkIdFavorite(json.id); 
            console.log(json.id);
            checkFavorite(true);
          }
      }
    
    } catch (error) {
      console.error(error);
    }
  }

  useEffect(() => {     
      onScreenLoad();
  }, [])

  const myFavorite = {
    restaurantId: data.id,
    restaurant: null,
    userId: 1,
    user: null,
  };

  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(myFavorite)
  };

  const addFav = async () => {
    try {
    const data = await fetch(api.POSTFAV, requestOptions)
    .then(response => response.json());
     //navigation.navigate('TabTwo')
    } catch (error) {
        console.error(error);
    }
  }

  const requestOptionsDelete = {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' }
  };

  const delFav = async () => {
    try {
    const data = await fetch('https://api.golun.ch/api/fav/'+ idFavorite, requestOptionsDelete)
    .then(() => console.log('Favoris Supprimé'))
    .then(() => navigation.navigate('TabFour'))
    } catch (error) {
        console.error(error);
    }
  }

  const onPressFav = () => {
    if(isFavorite == true) {
      checkFavorite(!isFavorite)
      delFav();
    }
    if(isFavorite == false) {
      checkFavorite(!isFavorite)
      addFav();
    }
  };
    
return (
    <View style={styles.container}>
        {data.restoPhoto ? (
          <Image
          // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
          style={{ width: 300, height: 300,  borderRadius: 10  }}
          source={{uri: data.restoPhoto}}
          />
          ) : (
            <Image
            // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
            style={{ width: 200, height: 200,  borderRadius: 10  }}
            source={require('../assets/images/no_photo.jpg')}
          />
          )}
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 32,}}>{data.nom}</Text>
    {data.entree ? (
    <Text>{data.description}</Text>
    ) : null}
    <Text>{data.localisation.cp} {data.localisation.ville}, {data.localisation.canton} - {data.localisation.pays}</Text>
    {data.dessert ? (
    <Text>Dessert : {data.dessert}</Text>
    ) : null}
    <Text>
        {data.inclusCafe == true ? 'Café compris' : null }
        {data.inclusBoisson == true ? 'Boisson comprise' : null }
    </Text>
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 16, marginBottom:50}}>{data.description}</Text>

    <TouchableOpacity
                      style={styles.icon}
                      onPress={onPressFav}
                    >
                      <MaterialIcons
                        name={isFavorite ? 'favorite' : 'favorite-outline'}
                        size={50}
                        color={'#EE7179'}
                      />
                    </TouchableOpacity>

                <TouchableOpacity
              style={isFavorite ? styles.btnOn : styles.btnOff }
              onPress={onPressFav}
            >
              <Ionicons name="heart" size={50} style={ isFavorite ? styles.iconOn : styles.iconOff } />
              <Text style={isFavorite ? styles.iconOn : styles.iconOff}>{isFavorite ? 'Favoris' : 'Non Favoris'}</Text>
            </TouchableOpacity>
    
    <View style={styles.footer}>
    
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 32}}>Contact</Text>
    <View style={{flex:1, flexDirection: 'row'}} >
    </View>
    <View style={{marginBottom: 20}}><Button title={'Contacter'} onPress={createTwoButtonAlert} color="#79B788" /></View>
    </View>
    </View>
    );
}
const styles = StyleSheet.create({
    container: {
      flex: 1,
      alignItems: 'center',
      backgroundColor: '#FFF'

    },
    title: {
      fontSize: 20,
      fontWeight: 'bold',
      fontFamily: 'DonaAlt-Medium'
    },
    btnPlusMinus: {
        fontSize: 32,
        backgroundColor: "#79B788",
    },
    footer: {
        flex: 3,
        width:'100%',
        height:100,
        alignItems:'center',
        justifyContent:'center',
        backgroundColor: '#79B788',
        borderTopLeftRadius: 30,
        borderTopRightRadius: 30,
        paddingHorizontal: 20,
        paddingVertical: 30,
      },
      icon: {
        //flex: 2,
        //width:'50%',
        alignItems: 'center',
        position: 'absolute'
      },
      iconOn:{
        //flex: 1,
        color: "#EE7179"
      },
      iconOff:{
        //flex: 1,
        color: "lightgray"
      },
      btnOn:{
        borderColor: "#EE7179",
        //flex: 1,
        minHeight:100,
        maxHeight:100,
        borderWidth: 2,
        paddingHorizontal:8,
        paddingVertical:10,
        alignItems: "center",
        marginVertical: 10,
        marginHorizontal: 5,
        borderRadius: 3
      },
      btnOff:{
        borderColor: "lightgray",
        //flex: 1,
        minHeight:100,
        maxHeight:100,
        borderWidth: 2,
        paddingHorizontal:8,
        paddingVertical:10,
        alignItems: "center",
        marginVertical: 10,
        marginHorizontal: 5,
        borderRadius: 3
      },
  });