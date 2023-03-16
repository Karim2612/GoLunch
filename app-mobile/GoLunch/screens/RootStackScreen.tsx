import React from 'react';

import { createStackNavigator } from '@react-navigation/stack';

import DiscoverScreen from './DiscoverScreen';
import LoginScreen from './LoginScreen';
import RegisterScreen from './RegisterScreen';

const RootStack = createStackNavigator();

const RootStackScreen = ({navigation}) => (
    <RootStack.Navigator headerMode='none'>
        <RootStack.Screen name="DiscoverScreen" component={DiscoverScreen}/>
        <RootStack.Screen name="LoginScreen" component={LoginScreen}/>
        <RootStack.Screen name="RegisterScreen" component={RegisterScreen}/>
    </RootStack.Navigator>
);

export default RootStackScreen;