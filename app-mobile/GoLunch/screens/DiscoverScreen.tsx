import React, {useState, useContext} from 'react';

// import all the components we are going to use
import {
  SafeAreaView,
  StyleSheet,
  View,
  Text,
  Image,
  Button,
  ImageBackground,
  TouchableOpacity
} from 'react-native';
import AppIntroSlider from 'react-native-app-intro-slider';
import { Ionicons } from '@expo/vector-icons'; 
import { AuthContext, AuthProvider } from '../context/auth';
import { MaterialCommunityIcons, Entypo } from '@expo/vector-icons'; 

const App = ({ navigation }) => {
  const { state, dispatch } = React.useContext(AuthContext);

  const [showRealApp, setShowRealApp] = useState(false);

  const onDone = () => {
    setShowRealApp(true);
  };
  const onSkip = () => {
    navigation.navigate('HomeScreen');
  };

  //Flo log la localisation toutes les 3 secondes
  //console.log({state});

  const RenderNextButton = () => {
    return (
      <View style={styles.buttonCircle}>
        <Ionicons name="arrow-forward-outline" size={24} color="rgba(255, 255, 255, .9)" />
      </View>
    );
  };

  const RenderDoneButton = () => {
    return (
      <View style={styles.buttonCircle}>
        <Ionicons name="checkmark" size={24} color="rgba(255, 255, 255, .9)" />
      </View>
    );
  };


  const RenderItem = ({item}) => {
    return (
      <View
        style={{
          flex: 1,
          backgroundColor: item.backgroundColor,
          alignItems: 'center',
          justifyContent: 'space-around',
          paddingBottom: 100,
        }}>
        <Text style={styles.introTitleStyle}>
          {item.title}
        </Text>
        <Image
          style={styles.introImageStyle}
          source={item.image} />
        <Text style={styles.introTextStyle}>
          {item.text}
        </Text>
      </View>
    );
  };

  return (
    <>
      {showRealApp ? (
        <SafeAreaView style={styles.container}>
          <ImageBackground source={require('../assets/images/map_discover.png')} style={styles.backgroundimage} resizeMode="cover">    
          <View style={styles.container}>
          <TouchableOpacity style={[styles.btn,{ borderColor: '#3A3B3C',backgroundColor: '#3A3B3C',borderWidth: 1,marginBottom: 55, height: 55, width:'70%'},]}
            onPress={() => navigation.navigate('HomeScreen')}>
            <Image
              source={require('../assets/images/golunch_logo.png')}
              style={{
                height: 47,
                width: 200,
                paddingVertical: 40,
              }}
              resizeMode={'contain'}
            />
            </TouchableOpacity>

            <Text style={{ fontSize: 26, marginBottom: 30, fontFamily: 'DonaAlt-Medium'}}>
              Hey !
            </Text>
            <Text style={styles.paragraphStyle}>
              GoLunch a besoin de ta localisation, Merci de l'activer dans les paramètres. 
            </Text>

            {(() => {
              if (Object.keys(state.userLocation.latitude).length == 0){
                  return (
                    <Text style={styles.paragraphStyle}>
                      Vous devez activer la géolocalisation pour utiliser GoLunch.
                    </Text>
                  )
              } else { 
                return (
                  <Text style={styles.paragraphStyle}>
                        lat:{state.userLocation.latitude} long:{state.userLocation.longitude}
                  </Text>
                )
              }
            })()}

            <TouchableOpacity style={[styles.btn,{ borderColor: '#79B788',backgroundColor: '#79B788',borderWidth: 1,marginTop: 20},]}
            onPress={() => navigation.navigate('HomeScreen')}>
            <Text style={[ styles.textBtn,{color: '#ffffff',},]}>
              Ok, j'ai Compris <Entypo name="check" size={24} color="white" />
            </Text>
            </TouchableOpacity>

            <TouchableOpacity style={[styles.btn,{ marginTop: 5},]}
            onPress={() => setShowRealApp(false)}>
            <Text style={[ styles.textBtn,{color: 'black', fontSize: 14},]}>
              Revoir les explications 
            </Text>
            </TouchableOpacity>

            <TouchableOpacity style={[styles.btn,{ borderColor: '#FABD02',backgroundColor: '#FABD02',borderWidth: 1,marginTop: 50, marginBottom: 30},]}
            onPress={() => {} }>
            <Text style={[ styles.textBtn,{color: '#ffffff',},]}>
              Jouer a PacMan <MaterialCommunityIcons name="death-star-variant" size={24} color="white" />
            </Text>
            </TouchableOpacity>

          </View>
          </ImageBackground>
        </SafeAreaView>
      ) : (
        <AppIntroSlider
          data={slides}
          renderItem={RenderItem}
          onDone={onDone}
          showSkipButton={true}
          onSkip={onSkip}
          renderDoneButton={RenderDoneButton}
          renderNextButton={RenderNextButton}
          // bottomButton
        />
      )}
    </>
  );
};

export default App;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'flex-end',
  },
  backgroundimage: {
    flex:1,
    justifyContent: 'flex-start',
  },
  titleStyle: {
    padding: 10,
    textAlign: 'center',
    fontSize: 18,
    fontWeight: 'bold',
  },
  paragraphStyle: {
    padding: 20,
    textAlign: 'center',
    fontSize: 16,
  },
  introImageStyle: {
    width: 200,
    height: 200,
  },
  introTextStyle: {
    fontSize: 18,
    color: 'white',
    textAlign: 'center',
    paddingVertical: 30,
  },
  introTitleStyle: {
    fontSize: 25,
    color: 'white',
    textAlign: 'center',
    marginBottom: 16,
    fontWeight: 'bold',
  },
  buttonCircle: {
    width: 40,
    height: 40,
    backgroundColor: 'rgba(0, 0, 0, .2)',
    borderRadius: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
  btn: {
    width: '70%',
    height: 45,
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 10,
  },
  textBtn: {
    fontSize: 18,
    fontWeight: 'bold',
  },
});

const slides = [
  {
    key: 's1',
    text: 'Trouvez les menus du jours \n près de chez vous.',
    title: 'Bienvenue sur GoLunch',
    image: {
      uri:
      'https://golun.ch/img/menu-board-intro.png',
    },
    backgroundColor: '#79B788',
  },
  {
    key: 's2',
    title: 'Manger sainement',
    text: 'Dénichez les meilleurs \n plats en 1 clic.',
    image: {
      uri:
      'https://golun.ch/img/food-intro.png',
    },
    backgroundColor: '#84bd92',
  },
  {
    key: 's3',
    title: 'Localisation',
    text: 'Découvrez les endroits\n sympas autour de vous.',
    image: {
      uri:
        'https://golun.ch/img/map-intro.png',
    },
    backgroundColor: '#8fc39c',
  }
];