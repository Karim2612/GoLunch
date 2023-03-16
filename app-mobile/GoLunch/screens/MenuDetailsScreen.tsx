import React, { useEffect, useState } from 'react';
import { Button, Image, View, Text, TouchableOpacity, StyleSheet, Alert } from 'react-native'
import { AntDesign } from '@expo/vector-icons'; 

export default function MenuDetailsScreen({ route, navigation }) { 
    const { data } = route.params;
    const [total, setTotal] = useState(1)

  const increase = () => {
    setTotal(total + 1)
  }

  const decrease = () => {
    setTotal(total - 1)
  }

  const createTwoButtonAlert = () =>
    Alert.alert('Merci pour votre réservation! ', 'Réserveration pour '+total+' personnes effectuée, bonne appétit et à bientôt :)', [
      {
        text: 'Fermer',
        onPress: () => console.log('Cancel Pressed'),
        style: 'cancel',
      },
      { text: 'OK', onPress: () => console.log('OK Pressed') },
    ]);
    
return (
    <View style={styles.container}>
        {data.platPhoto ? (
          <Image
          // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
          style={{ width: 300, height: 300,  borderRadius: 10, marginBottom: 20  }}
          source={{uri: data.platPhoto}}
          />
          ) : (
            <Image
            // style={{ width: "100%", height: "80%",  borderRadius: 10  }}
            style={{ width: 200, height: 200,  borderRadius: 10  }}
            source={require('../assets/images/no_photo.jpg')}
          />
          )}
    {/* <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 32,}}>Menu détails</Text> */}
    {data.entree ? (
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 16,}}>Entrée : {data.entree}</Text>
    ) : null}
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 16,}}>Plat : {data.plat}</Text>
    {data.dessert ? (
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 16,}}>Dessert : {data.dessert}</Text>
    ) : null}
    <Text>
        {data.inclusCafe == true ? 'Café compris' : null }
        {data.inclusBoisson == true ? 'Boisson comprise' : null }
    </Text>
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 32, marginBottom:50}}>{data.prix} CHF</Text>
    
    <View style={styles.footer}>
    <Text style={{fontFamily: 'DonaAlt-Medium', fontSize: 32}}>Réservation</Text>
    <View style={{flex:1, flexDirection: 'row'}} >
    <TouchableOpacity onPress={decrease}>
      <AntDesign name="minussquare" size={28} color="#fff" />
      </TouchableOpacity>
      <Text style={{fontSize: 20, paddingHorizontal:10,}}>{total} personne{total >= 2 ? 's':null}</Text>
      <TouchableOpacity onPress={increase}>
      <AntDesign name="plussquare" size={28} color="#fff" />
      </TouchableOpacity>
    </View>
    <View style={{marginBottom: 20}}><Button title={'Réserver'} onPress={createTwoButtonAlert} color="#79B788" /></View>
    <View><Text>Total : {data.prix * total} CHF</Text></View>
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
  });
  