import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import AntDesign from "@expo/vector-icons/AntDesign";

// Screens
import FormList from "../screens/Form/FormList";
import FormRegisterScreen from "../screens/Form/FormRegisterScreen";
import FormUpdateScreen from "../screens/Form/FormUpdateScreen";

import ModuleList from "../screens/Module/ModuleList";
import ModuleRegisterScreen from "../screens/Module/ModuleRegisterScreen";
import ModuleUpdateScreen from "../screens/Module/ModuleUpdateScreen";

import RolList from "../screens/Rol/RolList";
import RolRegisterScreen from "../screens/Rol/RolRegisterScreen";
import RolUpdateScreen from "../screens/Rol/RolUpdateScreen";

import PermissionList from "../screens/Permission/PermissionList";
import PermissionRegisterScreen from "../screens/Permission/PermissionRegisterScreen";
import PermissionUpdateScreen from "../screens/Permission/PermissionUpdateScreen";
import PermissionDeleteScreen from "../screens/Permission/PermissionDeleteScreen";

// Stacks
const FormStackNavigator = createNativeStackNavigator();
const ModuleStackNavigator = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

function FormStack() {
  return (
    <FormStackNavigator.Navigator>
      <FormStackNavigator.Screen name="FormList" component={FormList} options={{ title: "Lista de Formularios" }} />
      <FormStackNavigator.Screen name="FormRegister" component={FormRegisterScreen} options={{ title: "Registrar Formulario" }} />
      <FormStackNavigator.Screen name="FormUpdate" component={FormUpdateScreen} options={{ title: "Actualizar Formulario" }} />
      <FormStackNavigator.Screen name="FormDelete" component={FormUpdateScreen} options={{ title: "Eliminar Formulario" }} />
    </FormStackNavigator.Navigator>
  );
}

function ModuleStack() {
  return (
    <ModuleStackNavigator.Navigator>
      <ModuleStackNavigator.Screen name="ModuleList" component={ModuleList} options={{ title: "Lista de Módulos" }} />
      <ModuleStackNavigator.Screen name="ModuleRegister" component={ModuleRegisterScreen} options={{ title: "Registrar Módulo" }} />
      <ModuleStackNavigator.Screen name="ModuleUpdate" component={ModuleUpdateScreen} options={{ title: "Actualizar Módulo" }} />
      <ModuleStackNavigator.Screen name="ModuleDelete" component={ModuleUpdateScreen} options={{ title: "Eliminar Módulo" }} />
    </ModuleStackNavigator.Navigator>
  );
}

function RolStack() {
  return (
    <ModuleStackNavigator.Navigator>
      <ModuleStackNavigator.Screen name="RolList" component={RolList} options={{ title: "Lista de Roles" }} />
      <ModuleStackNavigator.Screen name="RolRegister" component={RolRegisterScreen} options={{ title: "Registrar Roles" }} />
      <ModuleStackNavigator.Screen name="RolUpdate" component={RolUpdateScreen} options={{ title: "Actualizar Roles" }} />
      <ModuleStackNavigator.Screen name="RolDelete" component={RolUpdateScreen} options={{ title: "Eliminar Roles" }} />
    </ModuleStackNavigator.Navigator>
  );
}

function PermissionStack() {
  return (
    <ModuleStackNavigator.Navigator>
      <ModuleStackNavigator.Screen name="PermissionList" component={PermissionList} options={{ title: "Lista de permisos" }} />
      <ModuleStackNavigator.Screen name="PermissionRegister" component={PermissionRegisterScreen} options={{ title: "Registrar permisos" }} />
      <ModuleStackNavigator.Screen name="PermissionUpdate" component={PermissionUpdateScreen} options={{ title: "Actualizar permisos" }} />
      <ModuleStackNavigator.Screen name="PermissionDelete" component={PermissionDeleteScreen} options={{ title: "Eliminar permisos" }} />
    </ModuleStackNavigator.Navigator>
  );
}

export default function Navigation() {
  return (
    <NavigationContainer>
      <Tab.Navigator screenOptions={{ tabBarActiveTintColor: "black" }}>
        <Tab.Screen
          name="FormTab"
          component={FormStack}
          options={{
            tabBarLabel: "Formularios",
            headerShown: false,
            tabBarIcon: ({ color, size }) => (
              <AntDesign name="form" size={size} color={color} />
            ),
          }}
        />

        <Tab.Screen
          name="RolTab"
          component={RolStack}
          options={{
            tabBarLabel: "Roles",
            headerShown: false,
            tabBarIcon: ({ color, size }) => (
              <AntDesign name="form" size={size} color={color} />
            ),
          }}
        />

        <Tab.Screen
          name="ModuleTab"
          component={ModuleStack}
          options={{
            tabBarLabel: "Módulos",
            headerShown: false,
            tabBarIcon: ({ color, size }) => (
              <AntDesign name="setting" size={size} color={color} />
            ),
          }}
        />

        <Tab.Screen
          name="PermissionTab"
          component={PermissionStack}
          options={{
            tabBarLabel: "Permisos",
            headerShown: false,
            tabBarIcon: ({ color, size }) => (
              <AntDesign name="setting" size={size} color={color} />
            ),
          }}
        />
      </Tab.Navigator>
    </NavigationContainer>
  );
}
