
//import the screens
import HomeScreen from './../screens/homeScreen'
import DetailsScreen from './../screens/detailsScreen'
import StacksScreen from './../screens/stacksScreen'
import SettingScreen from './../screens/settingScreen'

//icons
import Entypo from '@expo/vector-icons/Entypo';
import MaterialCommunityIcons from '@expo/vector-icons/MaterialCommunityIcons';
import FontAwesome5 from '@expo/vector-icons/FontAwesome5';
import Ionicons from '@expo/vector-icons/Ionicons';

//facilitar la navegacion
import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createNativeStackNavigator } from '@react-navigation/native-stack';

//Importar stacks
const HomeStackNavigator = createNativeStackNavigator();



//crea objetos del botton menu
const Tab=createBottomTabNavigator();

//crear la funcion cargar el objeto tab
function MyTabs(){
    return(
        <Tab.Navigator>
            <Tab.Screen name="Home" component={MyStacks}
              options={{
                    tabBarIcon: ({color, size}) => (
                          <Entypo name="home" size={24} color="black" />
                    ),
                    tabBarBadge: 3
                }}
            />
            <Tab.Screen name="Details" component={DetailsScreen}
            options={{
                    tabBarIcon: ({color, size}) => (
                          <MaterialCommunityIcons name="details" size={24} color="black" />
                    )
                }}
            />
            <Tab.Screen name="Stacks" component={StacksScreen}
              options={{
                    tabBarIcon: ({color, size}) => (
                          <FontAwesome5 name="dog" size={24} color="black" />
                    )
                }}
            />
            <Tab.Screen name="Setting" component= {SettingScreen}
            options={{
                    tabBarIcon: ({color, size}) => (
                          <Ionicons name="settings" size={24} color="black" />
                    )
                }}
            />
        </Tab.Navigator>
    );
}

//Stacks
function MyStacks(){
    return(
        <HomeStackNavigator.Navigator initialRouteName='Home'>
            <HomeStackNavigator.Screen name="Home" component={HomeScreen }/>
            <HomeStackNavigator.Screen name="Stack" component={StacksScreen }/>
            <HomeStackNavigator.Screen name="Detaills" component={DetailsScreen }/>
        </HomeStackNavigator.Navigator>
    );
}

export default function Navigation(){
    return(
    <NavigationContainer>
        <MyTabs/>
    </NavigationContainer>
    );
}

