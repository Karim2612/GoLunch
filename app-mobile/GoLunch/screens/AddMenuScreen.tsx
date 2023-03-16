import { StatusBar } from 'expo-status-bar';
import { Platform, StyleSheet, TouchableOpacity, TextInput, Button, Image, ScrollView, ImageBackground, View, Text, Keyboard } from 'react-native';
import React, { useState, useEffect } from 'react';
import { RootTabScreenProps } from '../types';
import { MaterialIcons, MaterialCommunityIcons } from '@expo/vector-icons';

import * as api from '../constants/Api';

export default function AddMenuScreen({ navigation }) {
  const [postPlat, setPostPlat] = useState(null);
  const [plat, setPlat] = useState('');
  const [prix, setPrix] = useState('');
  const [currentDate, setCurrentDate] = useState('');
  const [entree, setEntree] = useState('');
  const [hasEntree, checkEntree] = useState(false);
  const [dessert, setDessert] = useState('');
  const [hasDessert, checkDessert] = useState(false);
  const [boisson, setBoisson] = useState(false);
  const [cafe, setCafe] = useState(false);
  

  useEffect(() => {
    var date = new Date().getDate(); //Current Date
    var month = new Date().getMonth() + 1; //Current Month
    var year = new Date().getFullYear(); //Current Year

    setCurrentDate(
      date + '.' + month + '.' + year
    );
  }, []);

  const myMenu = {
    entree: entree,
    plat: plat,
    dessert: dessert,
    prix: prix,
    platPhoto: "",
    inclusBoisson: boisson,
    inclusCafe: cafe
  };

  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(myMenu)
  };

  const postMenu = async () => {
    try {
    const data = await fetch(api.ADDMENU, requestOptions)
    .then(response => response.json())
    .then(data => setPostPlat(data.plat));
    navigation.navigate('TabTwo')
    } catch (error) {
        console.error(error);
    }
  }

  const [isKeyboardVisible, setKeyboardVisible] = useState(true);

useEffect(() => {
    Keyboard.addListener('keyboardDidShow', function() {
        setKeyboardVisible(false);
    });
    Keyboard.addListener('keyboardDidHide', function() {
        setKeyboardVisible(true);
    });
});

  return (
    <View style={styles.container}>
      
      <ImageBackground
          source={require('../assets/images/add-menu-blackboard.jpg')}
          style={styles.backgroundimage}
        >
        <ScrollView>
        <Text style={styles.title}>Menu du {currentDate}</Text>
        
        {hasEntree ? <TextInput
          style={styles.normalInput}
          placeholder="Entrée"
          placeholderTextColor={'white'}
          onChangeText={setEntree}
          value={entree}
        /> : null }

        <TextInput
          style={styles.menuInput}
          numberOfLines={10}
          multiline={true}
          placeholder="Votre plat du jour"
          placeholderTextColor={'white'}
          onChangeText={setPlat}
          value={plat}
        />

        {hasDessert ? <TextInput
          style={styles.normalInput}
          placeholder="Dessert"
          placeholderTextColor={'white'}
          onChangeText={setDessert}
          value={dessert}
        /> : null }

        <TextInput
          style={styles.normalInput}
          keyboardType="numeric"
          placeholder="Prix"
          placeholderTextColor={'white'}
          onChangeText={setPrix}
          value={prix}
        />
        <Text
          style={{
            paddingHorizontal: 15,
            paddingTop: 10,
            fontWeight: 'bold',
            fontSize:16,
            color: 'lightgray'
          }}
        >
          Inclus dans le Prix de votre menu du jour :
        </Text>
        <View style={{ flex: 1, flexDirection: "row", justifyContent: 'space-around', paddingHorizontal:8, maxHeight:120, backgroundColor: 'rgba(52, 52, 52, alpha)' }}>
        <TouchableOpacity
              style={hasEntree ? styles.btnOn : styles.btnOff }
              onPress={() => checkEntree(!hasEntree)}
            >
              <MaterialCommunityIcons
                name="leaf"
                size={50}
                style={ hasEntree ? styles.iconOn : styles.iconOff }
              />
              <Text style={{ color: "lightgrey"}}>{hasEntree ? 'Oui' : 'Entrée'}</Text>
            </TouchableOpacity>

            
            <TouchableOpacity
              style={hasDessert ? styles.btnOn : styles.btnOff }
              onPress={() => checkDessert(!hasDessert)}
            >
              <MaterialCommunityIcons
                name="cupcake"
                size={50}
                style={ hasDessert ? styles.iconOn : styles.iconOff }
              />
              <Text style={{ color: "lightgrey"}}>{hasDessert ? 'Oui' : 'Dessert'}</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={boisson ? styles.btnOn : styles.btnOff }
              onPress={() => setBoisson(!boisson)}
            >
              <MaterialIcons
                name="local-drink"
                size={50}
                style={ boisson ? styles.iconOn : styles.iconOff }
              />
              <Text style={{ color: "lightgrey"}}>{boisson ? 'Oui' : 'Boisson'}</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={cafe ? styles.btnOn : styles.btnOff }
              onPress={() => setCafe(!cafe)}
            >
              <MaterialCommunityIcons
                name="coffee"
                size={50}
                style={ cafe ? styles.iconOn : styles.iconOff }
              />
              <Text style={{ color: "lightgrey"}}>{cafe ? 'Oui' : 'Café'}</Text>
            </TouchableOpacity>

          </View>

        <View style={{ flex: 1, paddingHorizontal: 15, paddingTop: 5 }}>
          <Button
            title="ADD MENU"
            color="#79B788"
            onPress={postMenu}
          />
        </View>
        </ScrollView>
        {isKeyboardVisible && (
        <Button onPress={() => navigation.goBack()} title="Dismiss" color="#EE7179" />
        )}
        </ImageBackground>
      {/* Use a light status bar on iOS to account for the black space above the modal */}
      <StatusBar style={Platform.OS === 'ios' ? 'light' : 'auto'} />
      
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#333333'
  },
  backgroundimage: {
    flex:1,
    justifyContent: 'flex-start',
    resizeMode:'stretch'
  },
  iconOn:{
    flex: 1,
    color: "#79B788"
  },
  iconOff:{
    flex: 1,
    color: "lightgray"
  },
  btnOn:{
    borderColor: "#79B788",
    flex: 1,
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
    flex: 1,
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
  menuInput: {
    height: 120,
    justifyContent: "flex-start",
    borderColor: "lightgray",
    borderWidth: 2,
    marginHorizontal: 15,
    paddingLeft: 10,
    paddingVertical: 10,
    textAlignVertical: "top",
    marginTop: 10,
    borderRadius: 3,
    color: 'white'
  },
  normalInput: {
    borderColor: "lightgray",
    borderWidth: 2,
    marginHorizontal: 15,
    paddingLeft: 10,
    paddingVertical:10,
    marginTop: 10,
    borderRadius: 3,
    color: 'white'
  },
  buttonInclude: {
    flex: 1,
    maxHeight:100,
    borderWidth: 0.5,
    paddingHorizontal:8,
    paddingVertical:10,
    alignItems: "center",
    marginVertical: 8,
    marginHorizontal: 5,
    borderRadius: 3
  },
  title: {
    fontSize: 24, 
    fontWeight: "700", 
    paddingHorizontal: 15,
    marginTop:5,
    fontFamily: 'Roboto-Medium',
    color: '#79B788'
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
});
