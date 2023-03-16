import * as Font from 'expo-font';

const useFonts = async () => {
  await Font.loadAsync({
    Roboto_Medium: require('../assets/fonts/Roboto-Medium.ttf'), 
    Roboto_Regular: require('../assets/fonts/Roboto-Regular.ttf'), 
  });
};

export default useFonts;