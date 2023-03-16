import React from 'react';
import { Platform } from 'react-native';

//Flo > API URL for Android and iOS 
//Pour dev local
// if (Platform.OS === 'android') {
//     var api_localhost = 'http://10.0.2.2:5168/api';
// } else {
//     var api_localhost = 'http://localhost:5168/api';
// }
var api_localhost = 'https://api.golun.ch/api';
export const API_URL = api_localhost;

//API EndPoints
export const REGISTER = `${API_URL}/Auth/register`;
export const LOGIN = `${API_URL}/Auth/login`;
export const GETMENU = `${API_URL}/Menu/filtres`;
export const ADDMENU = `${API_URL}/Menu`;
export const GETMENUPLAT = `${API_URL}/Menu/{recherche}`;
export const GETPLACES = `${API_URL}/Localisation`;
export const GETFAV= `${API_URL}/fav/1`;
export const POSTFAV= `${API_URL}/fav`;
export const GETFAVID= `${API_URL}/fav/get/`;
export const GETRESTO= `${API_URL}/Restaurant`;