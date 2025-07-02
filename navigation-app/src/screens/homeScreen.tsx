import { types } from '@babel/core';
import { NativeStackView } from '@react-navigation/native-stack';
import { StatusBar, View, Text, StyleSheet, Pressable } from 'react-native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { HomeStackParamsList } from '../navigations/types'; 
import { useNavigation } from '@react-navigation/native';

type homeStackNavigator= NativeStackNavigationProp<
    HomeStackParamsList,"Home">;
export default function HomeScreen() {
    const navitaion = useNavigation<homeStackNavigator>();
  return (
    <View>
      <Text>Hello home Screen</Text>
      <Pressable
      onPress={() => navitaion.navigate("Details", {id:"1"})}
        style={styles.Pressable}>
      </Pressable>
    </View>
  );
}

const styles = StyleSheet.create({
  Pressable:{
    backgroundColor: '0000',
    paddingLeft: 5,
    width:80,
  },
});