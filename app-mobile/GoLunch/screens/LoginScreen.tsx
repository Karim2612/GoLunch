import React, { createContext, useReducer, useEffect, useMemo } from 'react';
import { 
    View, 
    Text, 
    TouchableOpacity, 
    TextInput,
    Platform,
    StyleSheet,
    Image,
    StatusBar,
    Alert,
    ImageBackground
} from 'react-native';
import * as Animatable from 'react-native-animatable';
import { FontAwesome, Feather } from '@expo/vector-icons';
import { useTheme } from 'react-native-paper';
import * as SecureStore from 'expo-secure-store';
import axios from 'axios';

import * as api from '../constants/Api';

import { AuthContext, AuthProvider } from '../context/auth';

const LoginScreen = ({ navigation }) => {
  const [data, setData] = React.useState({
    username: '',
    password: '',
    check_textInputChange: false,
    secureTextEntry: true,
    isValidUser: true,
    isValidPassword: true,
  });

  const { state, dispatch } = React.useContext(AuthContext);

  const authContext = useMemo(
    () => ({
      logIn: async ({ username, password }) => {
        const config = {
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ username, password }),
        };

        console.log('Envoi au backend:');
        console.log(JSON.stringify({ username, password }));

        try {
          const response = await axios.post(api.LOGIN, { username, password });
          //const response = await fetch(api.LOGIN, config)
          //.then(response => response.json())
          console.log('\n', response.status);
          await SecureStore.setItemAsync('userToken', response.data.tokenjwt);
          dispatch({ type: 'LOG_IN', token: response.data.tokenjwt });
          //Si Ok retourne la Home
          navigation.navigate('Home')
        } catch (err) {
          dispatch({ type: 'AUTH_ERROR', error: err.response.data.detail });
          console.log('\n', err.response.status);
          console.log('\n', err.response.data);
        }
      },
      clearErrorMessage: () => dispatch({ type: 'CLEAR_AUTH_ERROR' }),
    }),
    []
  );

  const { colors } = useTheme();
//Test différentes images
//const image =  {require('../assets/images/login-background.jpg')}
//const image =  {uri: "https://images.unsplash.com/photo-1529175283207-194a414b9ffa?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"}
//const image =  {uri: "https://images.unsplash.com/photo-1594834749740-74b3f6764be4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=691&q=80"}
//const image =  {uri: "https://images.unsplash.com/photo-1592861956120-e524fc739696?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"}

  const textInputChange = (val) => {
    if (val.trim().length >= 3) {
      setData({
        ...data,
        username: val,
        check_textInputChange: true,
        isValidUser: true,
      });
    } else {
      setData({
        ...data,
        username: val,
        check_textInputChange: false,
        isValidUser: false,
      });
    }
  };

  const handlePasswordChange = (val) => {
    if (val.trim().length >= 3) {
      setData({
        ...data,
        password: val,
        isValidPassword: true,
      });
    } else {
      setData({
        ...data,
        password: val,
        isValidPassword: false,
      });
    }
  };

  const updateSecureTextEntry = () => {
    setData({
      ...data,
      secureTextEntry: !data.secureTextEntry,
    });
  };

  const handleValidUser = (val) => {
    if (val.trim().length >= 3) {
      setData({
        ...data,
        isValidUser: true,
      });
    } else {
      setData({
        ...data,
        isValidUser: false,
      });
    }
  };

  const myLogin = {
    username: data.username,
    password: data.password,
  };

  /*const loginHandle = (userName, password) => {

        const foundUser = Users.filter( item => {
            return userName == item.username && password == item.password;
        } );

        if ( data.username.length == 0 || data.password.length == 0 ) {
            Alert.alert('Wrong Input!', 'Username or password field cannot be empty.', [
                {text: 'Okay'}
            ]);
            return;
        }

        if ( foundUser.length == 0 ) {
            Alert.alert('Invalid User!', 'Username or password is incorrect.', [
                {text: 'Okay'}
            ]);
            return;
        }
        LogIn(foundUser);
    } */

    return (
        <View style={styles.container}>
            <StatusBar backgroundColor='#79B788' barStyle="light-content"/>
        
        <ImageBackground source={require('../assets/images/login-background.jpg')} style={styles.backgroundimage}>    
        <View style={styles.header}>
            <Text style={[styles.text_header, {fontFamily: 'DonaAlt-Medium'}]}>Welcome! </Text> 
            <Text>{state.userToken == null ? null : 'Already Sign In 🔒'}</Text>           
        </View>
        <Animatable.View 
            animation="fadeInUpBig"
            style={[styles.footer, {
                backgroundColor: colors.background
            }]}
        >
          <Text
          style={[
            styles.text_footer,
            {
              color: colors.text,
            },
          ]}
        >
          Username
        </Text>
        <View style={styles.action}>
          <FontAwesome name='user-o' color={colors.text} size={20} />
          <TextInput
            placeholder='Your Username'
            placeholderTextColor='#666666'
            style={[
              styles.textInput,
              {
                color: colors.text,
              },
            ]}
            autoCapitalize='none'
            onChangeText={(val) => textInputChange(val)}
            onEndEditing={(e) => handleValidUser(e.nativeEvent.text)}
          />
          {data.check_textInputChange ? (
            <Animatable.View animation='bounceIn'>
              <Feather name='check-circle' color='#79B788' size={20} />
            </Animatable.View>
          ) : null}
        </View>
        {data.isValidUser ? null : (
          <Animatable.View animation='fadeInLeft' duration={500}>
            <Text style={styles.errorMsg}>
              Username must be 3 characters long.
            </Text>
          </Animatable.View>
        )}

        <Text
          style={[
            styles.text_footer,
            {
              color: colors.text,
              marginTop: 35,
            },
          ]}
        >
          Password
        </Text>
        <View style={styles.action}>
          <Feather name='lock' color={colors.text} size={20} />
          <TextInput
            placeholder='Your Password'
            placeholderTextColor='#666666'
            secureTextEntry={data.secureTextEntry ? true : false}
            style={[
              styles.textInput,
              {
                color: colors.text,
              },
            ]}
            autoCapitalize='none'
            onChangeText={(val) => handlePasswordChange(val)}
          />
          <TouchableOpacity onPress={updateSecureTextEntry}>
            {data.secureTextEntry ? (
              <Feather name='eye-off' color='grey' size={20} />
            ) : (
              <Feather name='eye' color='grey' size={20} />
            )}
          </TouchableOpacity>
        </View>
        {data.isValidPassword ? null : (
          <Animatable.View animation='fadeInLeft' duration={500}>
            <Text style={styles.errorMsg}>
              Password must be at least 4 characters long.
            </Text>
          </Animatable.View>
        )}

        <TouchableOpacity>
          <Text style={{ color: '#79B788', marginTop: 15 }}>
            Forgot password?
          </Text>
        </TouchableOpacity>
        <View style={styles.button}>
          <TouchableOpacity
            style={[
              styles.logIn,
              {
                borderColor: '#79B788',
                backgroundColor: '#79B788',
                borderWidth: 1,
                marginTop: 15,
              },
            ]}
            
            onPress={() => {
              authContext.logIn(myLogin);
            }}
          >
            <Text
              style={[
                styles.textActionButton,
                {
                  color: '#ffffff',
                },
              ]}
            >
              Login
            </Text>
          </TouchableOpacity>

          <TouchableOpacity
            onPress={() => navigation.navigate('Register')}
            style={[
              styles.logIn,
              {
                marginTop: 5,
              },
            ]}
          >
            <Text style={[styles.textSecondaryButton]}>
              Don't have an account?{' '}
              <Text style={{ color: '#79B788' }}>Sign Up</Text>
            </Text>
          </TouchableOpacity>
        </View>
      </Animatable.View>
      </ImageBackground>
    </View>
  );
};

export default LoginScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#79B788',
  },
  header: {
    flex: 1,
    justifyContent: 'center',
    fontFamily: 'Roboto-Medium',
    paddingHorizontal: 0,
    minHeight:200
  },
  backgroundimage: {
    flex:1,
    justifyContent: 'flex-start',
  },
  footer: {
    flex: 3,
    backgroundColor: '#fff',
    borderTopLeftRadius: 30,
    borderTopRightRadius: 30,
    paddingHorizontal: 20,
    paddingVertical: 30,
  },
  text_header: {
    color: '#fff',
    backgroundColor: '#79B788',
    width:180,
    paddingLeft:20,
    fontWeight: 'bold',
    fontSize: 30,
  },
  text_footer: {
    color: '#05375a',
    fontSize: 18,
  },
  action: {
    flexDirection: 'row',
    marginTop: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#f2f2f2',
    paddingBottom: 5,
  },
  actionError: {
    flexDirection: 'row',
    marginTop: 10,
    borderBottomWidth: 1,
    borderBottomColor: '#EE7179',
    paddingBottom: 5,
  },
  textInput: {
    flex: 1,
    marginTop: Platform.OS === 'ios' ? 0 : -12,
    paddingLeft: 10,
    color: '#05375a',
  },
  errorMsg: {
    color: '#EE7179',
    fontSize: 14,
  },
  button: {
    alignItems: 'center',
    marginTop: 50,
  },
  logIn: {
    width: '100%',
    height: 50,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 10,
  },
  textActionButton: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  textSecondaryButton: {
    fontSize: 14,
    fontWeight: 'bold',
    color: 'grey',
  },
});
