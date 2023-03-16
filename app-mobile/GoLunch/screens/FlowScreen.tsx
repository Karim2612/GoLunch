import { StatusBar } from 'expo-status-bar';
import React, { useState, useEffect } from 'react';
import { Platform, StyleSheet, Text, View, ScrollView, ImageBackground, } from 'react-native';
import ImagePickerComponent from "../components/MenuImagePicker";
import callGoogleVisionAsync from "../hooks/useOcr";



export default function FlowScreen() {
  return (
    <View style={styles.container}>
      <ImageBackground
          source={require('../assets/images/add-menu-blackboard.jpg')}
          style={styles.backgroundimage}
        >
        <ScrollView>
        <ImagePickerComponent onSubmit={callGoogleVisionAsync} />
        </ScrollView>
        </ImageBackground>
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
  title: {
    fontSize: 20,
    fontWeight: 'bold',
    color: 'white',
  },
});
