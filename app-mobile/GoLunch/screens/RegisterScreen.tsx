import React from 'react';
import {
  View,
  Text,
  Button,
  TouchableOpacity,
  Dimensions,
  TextInput,
  Platform,
  StyleSheet,
  ScrollView,
  StatusBar,
} from 'react-native';
import * as Animatable from 'react-native-animatable';
import { FontAwesome, Feather } from '@expo/vector-icons';
import * as SecureStore from 'expo-secure-store';
import { useTheme } from 'react-native-paper';
import axios from 'axios';

import * as api from '../constants/Api';
import { AuthContext } from '../context/auth';

const RegisterScreen = ({ navigation }) => {
  const [data, setData] = React.useState({
    username: '',
    password: '',
    confirm_password: '',
    email: '',
    check_textInputChange: false,
    check_emailChange: false,
    secureTextEntry: true,
    confirm_secureTextEntry: true,
  });

  const { state, dispatch } = React.useContext(AuthContext);

  const authContext = React.useMemo(
    () => ({
      signUp: async ({ username, password, email }) => {
        const config = {
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ username, password, email }),
        };

        console.log('Envoi au backend:');
        console.log(JSON.stringify({ username, password, email }));

        try {
          const response = await axios.post(api.REGISTER, {
            username,
            password,
            email,
          });
          console.log('\n', response.status);
          console.log('\n', response.data);
          // to log the user in after signup, store the token and dispatch the LOG_IN action
          //await SecureStore.setItemAsync('userToken', response.data.tokenjwt);
          //dispatch({ type: 'LOG_IN', token: response.data.tokenjwt });
          navigation.navigate('Login')
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

  const windowHeight = Dimensions.get('window').height;

  const { colors } = useTheme();

  const myRegister = {
    username: data.username,
    password: data.password,
    email: data.email,
  };

  const textInputChange = (val) => {
    if (val.length !== 0) {
      setData({
        ...data,
        username: val,
        check_textInputChange: true,
      });
    } else {
      setData({
        ...data,
        username: val,
        check_textInputChange: false,
      });
    }
  };

  const emailChange = (val) => {
    if (val.length !== 0) {
      setData({
        ...data,
        email: val,
        check_emailChange: true,
      });
    } else {
      setData({
        ...data,
        email: val,
        check_emailChange: false,
      });
    }
  };

  const handlePasswordChange = (val) => {
    setData({
      ...data,
      password: val,
    });
  };

  const handleConfirmPasswordChange = (val) => {
    setData({
      ...data,
      confirm_password: val,
    });
  };

  const updateSecureTextEntry = () => {
    setData({
      ...data,
      secureTextEntry: !data.secureTextEntry,
    });
  };

  const updateConfirmSecureTextEntry = () => {
    setData({
      ...data,
      confirm_secureTextEntry: !data.confirm_secureTextEntry,
    });
  };

  return (
    <View style={styles.container}>
      <StatusBar backgroundColor='#79B788' barStyle='light-content' />
      <View style={styles.header}>
        <Text style={styles.text_header}>Create account</Text>
      </View>
      <Animatable.View animation='fadeInUpBig' style={styles.footer}>
        <ScrollView>
          <Text style={styles.text_footer}>Username</Text>
          <View style={styles.action}>
            <FontAwesome name='user-o' color='#05375a' size={20} />
            <TextInput
              placeholder='Your Username'
              style={styles.textInput}
              autoCapitalize='none'
              onChangeText={(val) => textInputChange(val)}
            />
            {data.check_textInputChange ? (
              <Animatable.View animation='bounceIn'>
                <Feather name='check-circle' color='#79B788' size={20} />
              </Animatable.View>
            ) : null}
          </View>

          <Text
            style={[
              styles.text_footer,
              {
                marginTop: 35,
              },
            ]}
          >
            E-mail
          </Text>
          <View style={styles.action}>
            <FontAwesome name='envelope-o' color='#05375a' size={20} />
            <TextInput
              placeholder='Your E-mail'
              style={styles.textInput}
              autoCapitalize='none'
              keyboardType='email-address'
              onChangeText={(val) => emailChange(val)}
            />
            {data.check_emailChange ? (
              <Animatable.View animation='bounceIn'>
                <Feather name='check-circle' color='#79B788' size={20} />
              </Animatable.View>
            ) : null}
          </View>

          <Text
            style={[
              styles.text_footer,
              {
                marginTop: 35,
              },
            ]}
          >
            Password & Confirmation
          </Text>
          <View style={styles.action}>
            <Feather name='lock' color='#05375a' size={20} />
            <TextInput
              placeholder='Your Password'
              secureTextEntry={data.secureTextEntry ? true : false}
              style={styles.textInput}
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

          {/* <Text style={[styles.text_footer, {
                marginTop: 35
            }]}>Confirm Password</Text> */}
          <View
            style={[
              styles.action,
              {
                marginTop: 20,
              },
            ]}
          >
            <Feather name='lock' color='#05375a' size={20} />
            <TextInput
              placeholder='Confirm Your Password'
              secureTextEntry={data.confirm_secureTextEntry ? true : false}
              style={styles.textInput}
              autoCapitalize='none'
              onChangeText={(val) => handleConfirmPasswordChange(val)}
            />
            <TouchableOpacity onPress={updateConfirmSecureTextEntry}>
              {data.secureTextEntry ? (
                <Feather name='eye-off' color='grey' size={20} />
              ) : (
                <Feather name='eye' color='grey' size={20} />
              )}
            </TouchableOpacity>
          </View>
          <View style={styles.textPrivate}>
            <Text style={styles.color_textPrivate}>
              By signing up you agree to our
            </Text>
            <Text style={[styles.color_textPrivate, { fontWeight: 'bold' }]}>
              {' '}
              Terms of service
            </Text>
            <Text style={styles.color_textPrivate}> and</Text>
            <Text style={[styles.color_textPrivate, { fontWeight: 'bold' }]}>
              {' '}
              Privacy policy
            </Text>
          </View>
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
                authContext.signUp(myRegister);
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
                Register
              </Text>
            </TouchableOpacity>
          </View>
          <TouchableOpacity
            onPress={() => navigation.navigate('Login')}
            style={[
              styles.logIn,
              {
                marginTop: 5,
              },
            ]}
          >
            <Text style={styles.textSecondaryButton}>
              Already have an account?{' '}
              <Text style={{ color: '#79B788' }}>Login here</Text>
            </Text>
          </TouchableOpacity>
        </ScrollView>
      </Animatable.View>
    </View>
  );
};

export default RegisterScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#79B788',
  },
  header: {
    flex: 1,
    justifyContent: 'flex-end',
    paddingHorizontal: 20,
    paddingBottom: 50,
  },
  footer: {
    flex: Platform.OS === 'ios' ? 4 : 5,
    backgroundColor: '#fff',
    borderTopLeftRadius: 30,
    borderTopRightRadius: 30,
    paddingHorizontal: 20,
    paddingVertical: 30,
  },
  text_header: {
    color: '#fff',
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
  textInput: {
    flex: 1,
    marginTop: Platform.OS === 'ios' ? 0 : -12,
    paddingLeft: 10,
    color: '#05375a',
  },
  button: {
    alignItems: 'center',
    marginTop: 10,
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
  textPrivate: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    marginTop: 20,
  },
  color_textPrivate: {
    color: 'grey',
  },
});
