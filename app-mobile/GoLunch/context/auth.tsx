import React, {
  createContext,
  useReducer,
  useEffect,
  useMemo,
  Dispatch,
} from 'react';

// Exporte AuthContext comme objet de Context 
export const AuthContext = createContext(null);

// Status initial
const initialState = {
  isLoading: false,
  isLogout: true,
  userToken: null,
  userLocation: null,
  //errorMessage: '',
};

// reducer to manage auth state
const reducer = (prevState, action) => {
  switch (action.type) {
    case 'AUTH_ERROR':
      return {
        ...prevState,
        errorMessage: action.error,
      };
    case 'CLEAR_AUTH_ERROR':
      return {
        ...prevState,
        errorMessage: '',
      };
    case 'LOG_IN':
      return {
        ...prevState,
        isLogout: false,
        userToken: action.token,
      };
    case 'LOG_OUT':
      return {
        ...prevState,
        isLogout: true,
        userToken: null,
      };
    case 'RESTORE_TOKEN':
      return {
        ...prevState,
        userToken: action.token,
        isLoading: false,
      };
      case 'USER_LOCATION':
      return {
        ...prevState,
        userLocation: action.location
      };
    default:
      return initialState;
  }
};

export const AuthProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  return (
    <AuthContext.Provider value={{ state, dispatch }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;