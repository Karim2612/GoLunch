import * as ImagePicker from "expo-image-picker";
import React, { useState, useEffect } from "react";
import { Button, Image, View, Text, TouchableOpacity, StyleSheet } from "react-native";
import { MaterialIcons } from '@expo/vector-icons'; 
import { Camera } from 'expo-camera';
import { color } from "react-native-reanimated";

function ImagePickerComponent({ onSubmit }) {
  const [image, setImage] = useState(null);
  const [text, setText] = useState("Nouveau ! Scannez un menu du jour avec notre intelligence artificielle GoLunch Vision.\n Exemple : Life is a Garden, Dig IT.");
  const [prix, setPrix] = useState("");
  //const [text2, setText2] = useState("");
  const pickImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.All,
      base64: true,
    });

    console.log(result);

    if (!result.cancelled) {
      setImage(result.uri);
      setText("Calculs.. patientez :)");
      const responseData = await onSubmit(result.base64);
      var traitement = responseData.text;
      var traitement = traitement.replace(/(\r\n|\n|\r)/gm, " ");
      var price = traitement.match(/\d+\.?\d*/);
      setText(responseData.text);
      setPrix(price);
      console.log(responseData.text);
      //setText2(responseData.fullTextAnnotation);
    }
  };
  const takeImage = async () => {
    let result = await ImagePicker.launchCameraAsync({
      base64: true,
    });

    console.log(result);

    if (!result.cancelled) {
      setImage(result.uri);
      setText("Calculs.. patientez :)");
      const responseData = await onSubmit(result.base64);
      setText(responseData.text);
    }
  };
  return (
    <View>
      <View style={styles.boxbtn}>
      <TouchableOpacity
            style={[styles.btn,{borderColor: '#79B788',backgroundColor: '#79B788',borderWidth: 1,marginTop: 15,},]}
            onPress={pickImage}>
            <MaterialIcons name="insert-photo" size={40} color="#FFF" />
            <Text style={[styles.textBtn, { color: '#ffffff',},]}>
              Galerie
            </Text>
      </TouchableOpacity>
      <TouchableOpacity
            style={[styles.btn,{borderColor: '#79B788',backgroundColor: '#79B788',borderWidth: 1,marginTop: 15,},]}
            onPress={takeImage}>
            <MaterialIcons name="photo-camera" size={40} color="#FFF" />
            <Text style={[styles.textBtn, { color: '#ffffff',},]}>
              Appareil
            </Text>
      </TouchableOpacity>
      </View>
      <View style={styles.boximg}>
      <Text style={{color:'#FFF', fontSize: 16, marginBottom:10, paddingHorizontal: 20}}>{text.replace(/(\r\n|\n|\r)/gm, " ").replace(/\d+\.?\d*/, "")} {"\n"} {prix}</Text>
      {image && <Image
          source={{ uri: image }}
          style={{ width: 300, height: 200, resizeMode: "contain", borderRadius: 10 }}
        />}
      {!image && (
        <Image
        source={require("../assets/images/lifeisagarden.jpg")}
        style={{ width: 200, height: 180, resizeMode: "contain", borderRadius: 10 }}
      />
      )}
      </View>
    </View>
  );
}
export default ImagePickerComponent;

const styles = StyleSheet.create({
    boxbtn: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        marginBottom: 30
      },
    boximg: {
      //flex: 1,
      alignItems: 'center',
      fontSize: 18,
    },
    title: {
      fontSize: 20,
      fontWeight: 'bold',
      fontFamily: 'DonaAlt-Medium'
    },
    btn: {
      width: 150,
      justifyContent: 'center',
      alignItems: 'center',
      borderRadius: 10,
      paddingVertical:10
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