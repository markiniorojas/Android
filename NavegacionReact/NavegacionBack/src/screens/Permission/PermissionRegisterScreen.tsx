import React, { useState } from "react";
import { View, Alert, StyleSheet, TouchableOpacity, Text } from "react-native";
import { createEntity } from "../../api/apiForm";
import { PermissionTasckParamsList } from "../../navigations/types";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { useNavigation } from "@react-navigation/native";
import { IPermission } from "../../api/types/IPermission";
import PermissionScreen from "../../Components/PermissionScreen";

const PermissionRegisterScreen = () => {
  const [Permission, setPermission] = useState<IPermission>({
    id: 0,
    name: "",
    description: "",
    isdeleted: false,
  });

  const navigation = useNavigation<NativeStackNavigationProp<PermissionTasckParamsList>>()

  const handleChange = (name: keyof IPermission, value: string | boolean) => {
    setPermission({ ...Permission, [name]: value });
  };

  const registerPermission = async () => {
    if (Permission.name.trim() === "" || Permission.description.trim() === "") {
      Alert.alert("Error", "Por favor complete todos los campos.");
      return;
    }

    try {
      console.log("Enviando permiso...", Permission);
      await createEntity(Permission, "Permission");
      console.log("permiso creado");
      Alert.alert("Éxito", "Permiso registrado correctamente", [
        { text: "OK", onPress: () => navigation.goBack() }, // Vuelve a la lista
      ]);
    } catch (error) {
      console.error("Error en el registro:", error);
      Alert.alert("Error", `No se pudo registrar el permiso. ${error}`);
    }
  };


 return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Nuevo Permiso</Text>
        <Text style={styles.subtitle}>Completa la información del Permiso</Text>
      </View>

      <View style={styles.formContainer}>
        <PermissionScreen Permission={Permission} handleChange={handleChange} />
      </View>

      <View style={styles.buttonContainer}>
        <TouchableOpacity 
          style={styles.cancelButton} 
          onPress={() => navigation.goBack()}
        >
          <Text style={styles.cancelButtonText}>Cancelar</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.saveButton} onPress={PermissionRegisterScreen}>
          <Text style={styles.saveButtonText}>Guardar Permiso</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f8f9fa",
    padding: 20,
  },
  header: {
    marginBottom: 24,
    paddingBottom: 16,
  },
  title: {
    fontSize: 28,
    fontWeight: "700",
    color: "#2d3748",
    marginBottom: 4,
  },
  subtitle: {
    fontSize: 16,
    color: "#718096",
  },
  formContainer: {
    flex: 1,
    backgroundColor: "#ffffff",
    borderRadius: 12,
    padding: 20,
    marginBottom: 20,
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.05,
    shadowRadius: 8,
    elevation: 2,
    borderWidth: 1,
    borderColor: "#e2e8f0",
  },
  buttonContainer: {
    flexDirection: "row",
    gap: 12,
    paddingTop: 8,
  },
  cancelButton: {
    flex: 1,
    backgroundColor: "#e2e8f0",
    paddingVertical: 16,
    paddingHorizontal: 20,
    borderRadius: 10,
    alignItems: "center",
    borderWidth: 1,
    borderColor: "#cbd5e0",
  },
  cancelButtonText: {
    color: "#4a5568",
    fontSize: 16,
    fontWeight: "600",
  },
  saveButton: {
    flex: 2,
    backgroundColor: "#4a5568",
    paddingVertical: 16,
    paddingHorizontal: 20,
    borderRadius: 10,
    alignItems: "center",
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  saveButtonText: {
    color: "#ffffff",
    fontSize: 16,
    fontWeight: "600",
    letterSpacing: 0.5,
  },
});


export default PermissionRegisterScreen;
