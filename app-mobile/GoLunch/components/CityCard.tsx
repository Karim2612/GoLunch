import React from 'react';
import { Text, View, StyleSheet, Image, TouchableOpacity, Alert } from 'react-native';

interface IRecipeProps {
    cityName?: string;
    cityImg?: undefined;
  }
  
  interface IRecipeState {
  }

export default class CityCard extends React.Component<IRecipeProps, IRecipeState> {
  render() {
    return (
      <TouchableOpacity onPress={() => alert(this.props.cityName)}>
      <View style={{height:130, width:130, marginLeft:20, borderWidth:0.5, borderColor:'lightgray', borderRadius:10, backgroundColor:'#79B788'}}>
        <View style={{flex:3}}>
          <Image source={this.props.cityImg} style={{flex:1, width:null, height:null, resizeMode:'cover', borderTopLeftRadius:10, borderTopRightRadius:10}} />
        </View>
        <View style={{flex:1, paddingLeft:10, paddingTop: 5}}>
          <Text style={{ color:'#FFF', fontWeight:"700" }}>{this.props.cityName}</Text>
        </View>
      </View>
      </TouchableOpacity>
    );
  }
}

const styles = StyleSheet.create({
  
});