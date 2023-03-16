import { StatusBar } from 'expo-status-bar';
import React, { useState, useEffect } from 'react';
import { Platform, StyleSheet, Text, View, ScrollView, ImageBackground, TouchableOpacity } from 'react-native';
import ImagePickerComponent from "../components/MenuImagePicker";
import callGoogleVisionAsync from "../hooks/useOcr";
import { MaterialIcons, Ionicons } from '@expo/vector-icons';

// interface steps {
//   value: string
// }
// interface currentStep {
//   value: int
// }

//Flo passe any, any pour ne pas avoir a definir steps et currentstep TypeScript c'est de la M
export default class StepsScreen extends React.Component<any, any> {
	constructor(props){
		super(props);
		this.state ={
			currentStep: 0, /* 0 est le premier element */
			steps: ['Scan', 'Menu', 'Place', 'Publier']
		}
	}
	
	render() {
		const styles = StyleSheet.create({
			centerElement: {justifyContent: 'center', alignItems: 'center'},
      backgroundimage: {
        flex:1,
        justifyContent: 'flex-start',
        resizeMode:'stretch'
      },
		});
		
		const { steps, currentStep } = this.state;
    // const [currentDate, setCurrentDate] = useState('');

    // useEffect(() => {
    //   var date = new Date().getDate(); //Current Date
    //   var month = new Date().getMonth() + 1; //Current Month
    //   var year = new Date().getFullYear(); //Current Year
  
    //   setCurrentDate(
    //     date + '.' + month + '.' + year
    //   );
    // }, []);
		
		return (
			<View style={{ flex: 1, flexDirection: 'column', backgroundColor: '#f6f6f6'}}>
        <ImageBackground
          source={require('../assets/images/add-menu-blackboard.jpg')}
          style={styles.backgroundimage}
        >

				<View style={{alignItems: 'center'}}>
					<View style={{width: 280, height: 60, marginTop:10}}>
						<View style={{alignItems: 'center'}}>
							<View style={{height: 2, backgroundColor: '#79B788', width: 180, position: 'absolute', top: 13, zIndex: 10}} />
						</View>
						<View style={{flexDirection: 'row', width: '100%', position: 'absolute', zIndex: 20}}>
							{steps.map((label, i) =>
								<View key={i} style={{alignItems: 'center', width: 70}}>
									{i > currentStep && i != currentStep && /* Not selected */
										<View style={{alignItems: 'center', justifyContent: 'center', width: 30, height: 30, backgroundColor: '#fff', borderWidth: 2, borderColor: '#79B788', borderRadius: 15, marginBottom: 10}}>
											<Text style={{fontSize: 15, color: '#79B788'}}>{i+1}</Text>
										</View>
									}
									{i < currentStep && /* Checked */
										<View style={{alignItems: 'center', justifyContent: 'center', width: 30, height: 30, backgroundColor: '#0faf9a', borderWidth: 2, borderColor: '#0faf9a', borderRadius: 15, marginBottom: 10}}>
											<Ionicons name="md-checkmark" size={20} color="#fff" />
										</View>
									}
									{i == currentStep && /* Selected */
										<View style={{alignItems: 'center', justifyContent: 'center', width: 30, height: 30, backgroundColor: '#79B788', borderWidth: 2, borderColor: '#79B788', borderRadius: 15, marginBottom: 10}}>
											<Text style={{fontSize: 13, color: '#ffffff'}}>{i+1}</Text>
										</View>
									}
									<Text style={{fontSize: 12, color: '#fff'}}>{label}</Text>
								</View>
							)}
						</View>
					</View>
				</View>
				
				<View style={{}}>
					{currentStep == 0 &&
						<View style={{height: 630}}>
							<Text style={{fontSize: 30, color:'#fff', textAlign:'center', fontFamily: 'DonaAlt-Medium'}}>Scanner un menu <MaterialIcons name="fiber-new" size={32} color="#79B788" style={{}} /></Text>
              <ScrollView>
              <ImagePickerComponent onSubmit={callGoogleVisionAsync} />
              </ScrollView>
						</View>
					}	
					{currentStep == 1 &&	
						<View style={{height: 630}}>
							<Text style={{fontSize: 30, color:'#fff', textAlign:'center', fontFamily: 'DonaAlt-Medium'}}>Menu du jour</Text>
						</View>
					}	
					{currentStep == 2 &&	
						<View style={{height: 630}}>
              <Text style={{fontSize: 30, color:'#fff', textAlign:'center', fontFamily: 'DonaAlt-Medium'}}>Choix du Restaurant</Text>
            </View>
					}	
					{currentStep == 3 &&	
						<View style={{height: 630}}>
              <Text style={{fontSize: 30, color:'#fff', textAlign:'center', fontFamily: 'DonaAlt-Medium'}}>Confirmation</Text>
            </View>
					}
					<View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
						{currentStep > 0 ?
							<TouchableOpacity style={[styles.centerElement, {bottom: 10, left: 10, width: 80, height: 35, backgroundColor: '#ee5e30', elevation: 10, borderRadius: 10}]} onPress={() => {
								if(currentStep > 0){
									this.setState({currentStep: currentStep - 1});
								}
							}}>
								<Text style={{color: '#fff'}}>Retour</Text>
							</TouchableOpacity>
							: <Text> </Text>
						}
						{(currentStep+1) < steps.length /* add other conditions here */ &&
							<TouchableOpacity style={[styles.centerElement, {bottom: 10, right: 10, width: 80, height: 35, backgroundColor: '#ee5e30', elevation: 10, borderRadius: 10}]} onPress={() => {
								if((currentStep+1) < steps.length){
									this.setState({currentStep: currentStep + 1});
								}
							}}>
								<Text style={{color: '#fff'}}>Suivant</Text>
							</TouchableOpacity>
						}
						{(currentStep+1) == steps.length /* add other conditions here */ &&
							<TouchableOpacity style={[styles.centerElement, {bottom: 25, right: 10, width: 180, height: 50, backgroundColor: '#0faf9a', elevation: 10, borderRadius: 10}]} onPress={() => {
								console.log('Fin add menu');
							}}>
								<Text style={{color: '#fff', fontSize:32, fontFamily: 'DonaAlt-Medium'}}>Publier</Text>
							</TouchableOpacity>
						}
					</View>
				</View>
        </ImageBackground>
			</View>
		);
	}
}
