import React, { useEffect } from "react";
import { TouchableOpacity } from "react-native-gesture-handler";
import { Text, Image, View, StyleSheet, Dimensions } from "react-native";
import { MaterialIcons, MaterialCommunityIcons } from '@expo/vector-icons';

export default function CardMenu() {
  return (
    <>
      <TouchableOpacity style={styles.card}>
        <View style={{ flexDirection: "row"}}>
          <View style={styles.cardImage}>
          <Image
              style={{ width: "100%", height: "80%",  borderRadius: 10  }}
              source={require('../assets/images/food-example.jpg')}
            />
          </View>
          <View style={{ flex: 1 }}>
            <Text style={styles.cardTitle}>Title</Text>
            <Text style={styles.cardLocation}>location</Text>
            <Text style={styles.cardDescription}>Lorem ipsum dolor sit amet, consectetur adipiscing elit.  Lorem ipsum dolor sit amet.</Text>
            <View style={{ flexDirection: "row"}}>
            <MaterialCommunityIcons name="leaf" size={25}  style={ styles.iconOff}/>
            <MaterialCommunityIcons name="cupcake" size={25}  style={ styles.iconOff}/>
            <MaterialIcons name="local-drink" size={25} style={ styles.iconOff}/>
            <MaterialCommunityIcons name="coffee" size={25}  style={ styles.iconOff}/>
                </View>
            </View>
            <View style={{ flexDirection: "column" }}>
            <Text style={styles.cardPrice}>CHF 15</Text>
            </View>
          </View>
      </TouchableOpacity>
    </>
  );
}

const { width, height } = Dimensions.get("screen");

const styles = StyleSheet.create({
    card: {
        marginVertical: 10,
        backgroundColor: "#fff",
        paddingVertical: 15,
        paddingHorizontal: 15,
        width: width / 1.1,
        marginHorizontal: 20,
        borderRadius: 20,
        height: height / 6.5 ,
        shadowColor: "#000",
        shadowOffset: {
          width: 2,
          height: 2,
        },
        shadowOpacity: 0.3,
        shadowRadius: 1.5,
      },
    
      cardTitle: {
        fontWeight: "bold",
        fontSize: 15,
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
      } , 

      iconOff:{
        color: "lightgray",
        marginLeft: 10,
      },
    },
);

