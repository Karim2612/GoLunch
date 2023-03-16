import React, { useEffect, useState } from 'react';
import EditScreenInfo from '../components/EditScreenInfo';
import { TouchableOpacity } from "react-native-gesture-handler";
import { StyleSheet, ActivityIndicator, FlatList, Text, View, Platform, Button, Image, Dimensions, TextInput  } from 'react-native';
import { RootTabScreenProps } from '../types';
import CardMenu from '../components/CardMenu';
import { MaterialIcons, MaterialCommunityIcons } from '@expo/vector-icons';
import { useIsFocused } from '@react-navigation/native'



import * as api from '../constants/Api';

export default function TabFourScreen({ navigation }) {
  const [isLoading, setLoading] = useState(true);
  const [isFetching, setIsFetching] = useState(false);
  const [data, setData] = useState([]);
  const isFocused = useIsFocused();

  const fetchData = () => {
    getFavorites();
    setIsFetching(false);
  };
  
  const onRefresh = () => {
    setIsFetching(true);
    fetchData();
  };

  const getFavorites = async () => {
    try {
      const response = await fetch(api.GETFAV);
      const json = await response.json();
      setData(json);      
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  //TESTING id missing problem 
  //?JSON.stringify(value, replacer, space)
  /*const getFavorites = async () => {
  const response = await fetch(api.GETFAV);
  console.log(JSON.stringify(response, null, 4));
    return response.json();
  }*/

  useEffect(() => {
    getFavorites();
  }, [isFocused]);

  return (
    <View style={{ flex: 1, height: '100%', backgroundColor: '#FFF' }}>
       {/* <CardMenu/>  */}
      {isLoading ? (
        <ActivityIndicator />
      ) : (
        <>
        <Text style={{ fontSize: 26, paddingHorizontal: 15, paddingTop: 20, fontFamily: 'DonaAlt-Medium'}}>
              Mes Favoris
              </Text>
        <FlatList
          style={{ flex: 1, flexDirection: "column" }}
          contentContainerStyle={{alignItems:'flex-start'}}
          data={data}
          onRefresh={onRefresh}
          refreshing={isFetching}
          keyExtractor={({ id }, index) => id}
          renderItem={({ item }) => (

            <TouchableOpacity style={styles.card} activeOpacity={0.2} onPress={() => {
              navigation.navigate('RestoDetails', {data: item})}}>
        <View style={{ flexDirection: "row"}}>
          <View style={styles.cardImage}>
          {item.restoPhoto ? (
          <Image
          // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
          style={{ width: 80, height: 80,  borderRadius: 10  }}
          source={{uri: item.restoPhoto}}
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
            <Text style={styles.cardTitle}>{item.nom}</Text>
            <Text 
                      // Sam: Add array to render localisation (adresse, cp, canton etc.)
            style={styles.cardLocation}>{item.localisation.ville} - {item.localisation.cp}</Text> 
            <Text style={styles.cardDescription}>{item.description}</Text> 
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
});