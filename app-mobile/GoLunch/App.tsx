import { StatusBar } from 'expo-status-bar';
import React, { useEffect, useState } from "react"
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { LogBox } from 'react-native';
import 'react-native-gesture-handler';
import useCachedResources from './hooks/useCachedResources';
import useColorScheme from './hooks/useColorScheme';
import Navigation from './navigation';

//Flo enlève les messages d'erreurs du composant de gestion des gestes
LogBox.ignoreLogs([
  "[react-native-gesture-handler] Seems like you\'re using an old API with gesture components, check out new Gestures system!",
]);

export default function App() {
  const isLoadingComplete = useCachedResources();
  const colorScheme = useColorScheme();

  if (!isLoadingComplete) {
    return null;
  } else {
    return (
      <SafeAreaProvider>
        <Navigation />
        <StatusBar />
      </SafeAreaProvider>
    );
  }
}
