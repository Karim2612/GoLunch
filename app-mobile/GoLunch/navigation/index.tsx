import {
  FontAwesome,
  MaterialCommunityIcons,
  Entypo,
} from '@expo/vector-icons';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { createDrawerNavigator } from '@react-navigation/drawer';
import {
  NavigationContainer,
  DefaultTheme,
  DarkTheme,
} from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import * as React from 'react';
import { Button, ColorSchemeName, Pressable, Text, View } from 'react-native';

import Colors from '../constants/Colors';
import useColorScheme from '../hooks/useColorScheme';
import ModalScreen from '../screens/ModalScreen';
import NotFoundScreen from '../screens/NotFoundScreen';
import TabOneScreen from '../screens/TabOneScreen';
import TabTwoScreen from '../screens/TabTwoScreen';
import TabThreeScreen from '../screens/TabThreeScreen';
import TabFourScreen from '../screens/TabFourScreen';
import {
  RootStackParamList,
  RootTabParamList,
  RootTabScreenProps,
} from '../types';
import LinkingConfiguration from './LinkingConfiguration';
import AddMenuScreen from '../screens/AddMenuScreen';
import MenuDetailsScreen from '../screens/MenuDetailsScreen';
import FlowScreen from '../screens/FlowScreen';
import DiscoverScreen from '../screens/DiscoverScreen';
import LoadingScreen from '../screens/LoadingScreen';

import RegisterScreen from '../screens/RegisterScreen';
import LoginScreen from '../screens/LoginScreen';
import ProfileScreen from '../screens/ProfileScreen';
import EditProfileScreen from '../screens/EditProfileScreen';
import SettingsScreen from '../screens/SettingsScreen';
import CustomDrawer from '../components/CustomDrawer';

import StepsScreens from '../screens/StepsScreen';

import RestoListScreen from '../screens/RestoListScreen';
import RestoDetailsScreen from '../screens/RestoDetailsScreen';


import * as TaskManager from "expo-task-manager"
import * as Location from "expo-location"
import * as SecureStore from 'expo-secure-store';
import * as Linking from 'expo-linking';

import { AuthContext, AuthProvider } from '../context/auth';

export default () => {
  return (
    <AuthProvider>
      <NavigationContainer>
        <RootNavigator />
      </NavigationContainer>
    </AuthProvider>
  );
};

const LOCATION_TASK_NAME = "LOCATION_TASK_NAME"
let foregroundSubscription = null

// 
TaskManager.defineTask(LOCATION_TASK_NAME, async ({ data, error }) => {
  if (error) {
    console.error(error)
    return
  }
  if (data) {
    const { locations } = data
    const location = locations[0]
    if (location) {
      console.log("Location Update ", location.coords)
    }
  }
});

const Stack = createNativeStackNavigator();

const RootNavigator = () => {
  const { state, dispatch } = React.useContext(AuthContext);
  const [position, setPosition] = React.useState(null)

  React.useEffect(() => {
    const fetchToken = async () => {
      let userToken;
      console.log('Test si passe bien par là');
      try {
        userToken = await SecureStore.getItemAsync('userToken');
      } catch (err) {
        // Restoring token failed
        console.log(err);
        console.log('Pas de token enregistré.');
      } finally {
        console.log('Finallement')
        //dispatch({type: 'USER_LOCATION', location: 6.4587})
      }
      // After restoring token, we may need to validate it
      dispatch({ type: 'RESTORE_TOKEN', token: userToken });
    };
    fetchToken();
  }, [dispatch]);

  //Flo GEOLOCATION 
  // Demande la permission pour utiliser la géolocalisation
  React.useEffect(() => {
    const requestPermissions = async () => {
      const foreground = await Location.requestForegroundPermissionsAsync()
      startForegroundUpdate()
      // Demande la permission pour quand l'app est eteinte
      // if (foreground.granted) await Location.requestBackgroundPermissionsAsync()
    }
    requestPermissions()
  }, [])

  // Start location tracking
  const startForegroundUpdate = async () => {
    const permission = await Location.getForegroundPermissionsAsync()

    if (!permission.canAskAgain || permission.status === "denied") {
      console.log("location tracking denied")
    
      Linking.openSettings();
    } else {
      if (permission.status === "granted") {
        foregroundSubscription?.remove()

        foregroundSubscription = await Location.watchPositionAsync(
          {
            // Essayer d'essayer d'autres options que BestForNvigation 
            // timeinterval de 10sec sinon est en temps reel
            accuracy: Location.Accuracy.BestForNavigation,
            timeInterval: 5000,
          },
          location => {
            setPosition(location.coords)
            dispatch({ type: 'USER_LOCATION', location: location.coords });
            //console.log(location.coords)
          }
        )
      }
    }

  }

  if (state.isLoading) {
     // Recherche du token
     return <LoadingScreen />;
  }

  return (
    <Stack.Navigator>
      <Stack.Screen name="Root" component={DiscoverScreen} options={{ headerShown: false }} />
      <Stack.Screen
        name='HomeScreen'
        component={HomeScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name='NotFound'
        component={NotFoundScreen}
        options={{ title: 'Oops!' }}
      />
      <Stack.Screen
        name='MenuDetails'
        component={MenuDetailsScreen}
        options={{ title: 'Menu du jour :' }}
      />
      <Stack.Screen
        name='RestoDetails'
        component={RestoDetailsScreen}
        options={{ title: 'Restaurant' }}
      />
      <Stack.Screen
        name='EditProfile'
        component={EditProfileScreen}
        options={{ title: 'Edit profile' }}
      />
      <Stack.Group screenOptions={{ presentation: 'modal' }}>
        <Stack.Screen name='Modal' component={ModalScreen} />
        <Stack.Screen name='AddMenu' component={AddMenuScreen} options={{ title: 'Add a Menu', headerStyle: {
            backgroundColor: '#79B788',
          },
          headerTintColor: '#fff',
          headerTitleStyle: {
            fontWeight: 'bold',
          }, }} />
        <Stack.Screen name='AddMenuSteps' component={StepsScreens} options={{ title: 'Ajouter un menu', headerStyle: {
            backgroundColor: '#79B788',
          },
          headerTintColor: '#fff',
          headerTitleStyle: {
            fontWeight: 'bold',
          }, }} />
      </Stack.Group>
    </Stack.Navigator>
  );
};

// Flo : Création d'une route qui conduit vers le bottom tab

const Drawer = createDrawerNavigator();

function HomeScreen() {
  const { state, dispatch } = React.useContext(AuthContext);
  const colorScheme = useColorScheme();
  return (
    //Smr Initially it was  <Drawer.Navigator  initialRouteName="Home" <- Flo je l'ai remis
    <Drawer.Navigator
      initialRouteName='GoLunch'
      drawerContent={(props) => <CustomDrawer {...props} />}
      screenOptions={{
        //headerShown: false,
        drawerActiveBackgroundColor: '#79B788',
        drawerActiveTintColor: '#fff',
        drawerInactiveTintColor: '#333',
        drawerLabelStyle: {
          marginLeft: -25,
          fontFamily: 'Roboto-Medium',
          fontSize: 15,
        },
      }}
    >
      {/* {state.userToken == null ? ( */}
      <Drawer.Screen
        name='Login'
        component={LoginScreen}
        options={{
          drawerIcon: ({ color }) => (
            <FontAwesome name='sign-in' size={22} color={color} />
          ),
        }}
      />
      <Drawer.Screen
        name='Register'
        component={RegisterScreen}
        options={{
          drawerIcon: ({ color }) => (
            <FontAwesome name='user-plus' size={22} color={color} />
          ),
        }}
      />
      {/* ) : ( null )} */}
      <Drawer.Screen
        name='GoLunch'
        component={BottomTabNavigator}
        options={({ navigation }) => ({
          drawerIcon: ({ color }) => (
            <FontAwesome name='home' size={22} color={color} />
          ),
          headerRight: () => (
            <Pressable
              onPress={() => navigation.navigate('AddMenuSteps')}
              style={({ pressed }) => ({
                opacity: pressed ? 0.5 : 1,
                flexDirection:'row'
              })}
            >
              <FontAwesome
                name='plus-square'
                size={25}
                color={'#79B788'}
                style={{ marginRight: 3 }}
              /> 
              <Text style={{ marginRight: 15, fontSize : 16 }}>Menu</Text>
            </Pressable>
          ),
        })}
      />
      <Drawer.Screen
        name='Profile'
        component={ProfileScreen}
        options={({ navigation }) => ({
          drawerIcon: ({ color }) => (
            <FontAwesome name='user-circle' size={22} color={color} />
          ),
          headerRight: () => (
            <View style={{marginRight: 15}}>
            <Pressable
            //onPress={() => navigation.navigate('EditProfileScreen')}
            style={({ pressed }) => ({
              opacity: pressed ? 0.5 : 1,
            })}
          >
            <MaterialCommunityIcons.Button
                name="account-edit"
                size={25}
                backgroundColor="#fff"
                color="#000"
                onPress={() => navigation.navigate('EditProfile')}
              />
          </Pressable>
            </View>
          ),
        })}
      />
      <Drawer.Screen
        name='Settings'
        component={SettingsScreen}
        options={({ navigation }) => ({
          title: 'Paramètres',
          drawerIcon: ({ color }) => (
            <FontAwesome name='gear' size={22} color={color} />
          ),
          headerRight: () => (
            <Pressable
              onPress={() => navigation.navigate('Modal')}
              style={({ pressed }) => ({
                opacity: pressed ? 0.5 : 1,
              })}
            >
              <FontAwesome
                name='info-circle'
                size={25}
                color={Colors[colorScheme].text}
                style={{ marginRight: 15 }}
              />
            </Pressable>
          ),
        })}
      />
      <Drawer.Screen
        name='Restaurants'
        component={RestoListScreen}
        options={{
          drawerIcon: ({ color }) => (
            <FontAwesome name='building-o' size={22} color={color} />
          ),
        }}
      />
    </Drawer.Navigator>
  );
}

const BottomTab = createBottomTabNavigator();

function BottomTabNavigator() {
  const colorScheme = useColorScheme();

  return (
    <BottomTab.Navigator
      initialRouteName='TabOne'
      screenOptions={{
        tabBarActiveTintColor: Colors[colorScheme].tint,
      }}
    >
      <BottomTab.Screen
        name='TabOne'
        component={TabOneScreen}
        options={({ navigation }) => ({
          title: 'Accueil',
          headerShown: false,
          tabBarIcon: ({ color }) => (
            <Entypo name='home' size={30} color={color} />
          ),
          headerRight: () => (
            <Pressable
              onPress={() => navigation.navigate('Modal')}
              style={({ pressed }) => ({
                opacity: pressed ? 0.5 : 1,
              })}
            >
              <FontAwesome
                name='info-circle'
                size={25}
                color={Colors[colorScheme].text}
                style={{ marginRight: 15 }}
              />
            </Pressable>
          ),
        })}
      />
      <BottomTab.Screen
        name='TabTwo'
        component={TabTwoScreen}
        options={({ navigation }) => ({
          title: 'Menus du jour',
          headerShown: false,
          tabBarIcon: ({ color }) => (
            <FontAwesome size={28} name='cutlery' color={color} />
          ),
          headerRight: () => (
            <Pressable
              onPress={() => navigation.navigate('AddMenu')}
              style={({ pressed }) => ({
                opacity: pressed ? 0.5 : 1,
              })}
            >
              <Text>Add menu</Text>
              <FontAwesome
                name='plus-circle'
                size={25}
                color={Colors[colorScheme].text}
                style={{ marginRight: 15 }}
              />
            </Pressable>
          ),
        })}
      />
      <BottomTab.Screen
        name='TabThree'
        component={TabThreeScreen}
        options={{
          title: 'NearBy',
          headerShown: false,
          tabBarIcon: ({ color }) => (
            <Entypo name='location-pin' size={30} color={color} />
          ),
        }}
      />
      <BottomTab.Screen
        name='TabFour'
        component={TabFourScreen}
        options={{
          title: 'Favorites',
          headerShown: false,
          tabBarIcon: ({ color }) => (
            <FontAwesome size={25} name='heart' color={color} />
          ),
        }}
      />
      <BottomTab.Screen
        name='Flow'
        component={FlowScreen}
        listeners={({ navigation }) => ({
          tabPress: e => {
            e.preventDefault();
            navigation.toggleDrawer();
          }
        })}
        options={{
          title: 'More',
          headerShown: false,
          tabBarIcon: ({ color }) => (
            <Entypo name='dots-three-horizontal' size={30} color={color} />
          ),
        }}
      />
    </BottomTab.Navigator>
  );
}

/**
 * You can explore the built-in icon families and icons on the web at https://icons.expo.fyi/
 */
function TabBarIcon(props: {
  name: React.ComponentProps<typeof FontAwesome>['name'];
  color: string;
}) {
  return <FontAwesome size={30} style={{ marginBottom: -3 }} {...props} />;
}
