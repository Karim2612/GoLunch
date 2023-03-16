import React from 'react';
import {
  View,
  Text,
  ImageBackground,
  Image,
  StyleSheet,
  TouchableOpacity,
} from 'react-native';
import {
  DrawerContentScrollView,
  DrawerItemList,
} from '@react-navigation/drawer';
import { FontAwesome } from '@expo/vector-icons';
import * as SecureStore from 'expo-secure-store';
import { AuthContext, AuthProvider } from '../context/auth';

//custom drawer component
const CustomDrawer = (props, navigation) => {
  const { state, dispatch } = React.useContext(AuthContext);

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
  return (
    <View style={{ flex: 1 }}>
      <DrawerContentScrollView
        {...props}
        contentContainerStyle={{ backgroundColor: '#79B788' }}
      >
        <ImageBackground
          source={require('../assets/images/nav-bg.jpg')}
          style={{ padding: 20 }}
        >
          <Image
            source={require('../assets/images/user-profile.jpg')}
            style={{
              height: 80,
              width: 80,
              borderRadius: 40,
              marginBottom: 10,
            }}
          />
          <Text
            style={{
              color: '#fff',
              fontSize: 18,
              fontFamily: 'Roboto-Medium',
              marginBottom: 5,
            }}
          >
            Badgalriri
          </Text>
          <View style={{ flexDirection: 'row' }}>
            <Text
              style={{
                color: '#fff',
                fontFamily: 'Roboto-Regular',
                marginRight: 5,
              }}
            >
              9000 Coins
            </Text>
            <FontAwesome name='money' size={14} color='#fff' />
          </View>
        </ImageBackground>
        <View style={{ flex: 1, backgroundColor: '#fff', paddingTop: 10 }}>
          <DrawerItemList {...props} />
        </View>
      </DrawerContentScrollView>
      <View style={{ padding: 20, borderTopWidth: 1, borderTopColor: '#ccc' }}>
        <TouchableOpacity onPress={() => {}} style={{ paddingVertical: 15 }}>
          <View style={{ flexDirection: 'row', alignItems: 'center' }}>
            <FontAwesome name='info-circle' color={'#333'} size={22} />
            <Text
              style={{
                fontSize: 15,
                fontFamily: 'Roboto-Medium',
                marginLeft: 5,
                color: '#333',
              }}
            >
              About Us
            </Text>
          </View>
        </TouchableOpacity>
        {state.userToken == null ? (
        null
      ) : (
        <TouchableOpacity onPress={authContext.logOut} style={{ paddingVertical: 15 }}>
          <View style={{ flexDirection: 'row', alignItems: 'center' }}>
            <FontAwesome name='sign-out' color={'#333'} size={22} />
            <Text
              style={{
                fontSize: 15,
                fontFamily: 'Roboto-Medium',
                marginLeft: 5,
                color: '#333',
              }}
            >
              Sign Out
            </Text>
          </View>
        </TouchableOpacity>
      )}
      </View>
      <View style={styles.footer}>
        <Image
          style={{ marginLeft: 20, marginTop: 0 }}
          resizeMode='contain'
          source={require('../assets/images/swiss-icon.png')}
        />
        <Text style={styles.description}>GoLunch</Text>
        <Text style={styles.version}>v.1.0.42</Text>
      </View>
    </View>
  );
};

export default CustomDrawer;

const styles = StyleSheet.create({
  footer: {
    height: 50,
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#79B788',
    borderTopWidth: 1,
    borderTopColor: 'lightgray',
  },
  version: {
    flex: 1,
    textAlign: 'right',
    marginRight: 20,
    fontSize: 14,
    color: 'lightgrey',
  },
  description: {
    flex: 1,
    marginLeft: 10,
    fontSize: 14,
    color: 'white',
    fontWeight: 'bold',
  },
});
