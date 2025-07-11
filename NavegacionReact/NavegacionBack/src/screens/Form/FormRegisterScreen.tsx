import React, { useState } from "react";
import { View, TouchableOpacity, Text, Alert, StyleSheet } from "react-native";
import { IForm } from "../../api/types/IForm";
import FormularioScreen from "../../Components/FormularioScreen";
import { createEntity } from "../../api/apiForm";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { FormtackParamsList } from "../../navigations/types";

const FormRegisterScreen = () => {
  const [form, setForm] = useState<IForm>({
    id: 0,
    url: "",
    name: "",
    description: "",
    isdeleted: false,
  });

  const navigation = useNavigation<NativeStackNavigationProp<FormtackParamsList>>();

  const handleChange = (name: keyof IForm, value: string | boolean) => {
    setForm({ ...form, [name]: value });
  };

  const registerForm = async () => {
    if (form.name.trim() === "" || form.description.trim() === "") {
      Alert.alert("Error", "Por favor complete todos los campos.");
      return;
    }

    try {
      console.log("Enviando formulario...", form);
      await createEntity(form, "Form");
      Alert.alert("Éxito", "Formulario registrado correctamente.", [
        { text: "OK", onPress: () => navigation.goBack() }, 
      ]);
    } catch (error) {
      console.error("Error en el registro:", error);
      Alert.alert("Error", `No se pudo registrar el formulario. ${error}`);
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Nuevo Formulario</Text>
        <Text style={styles.subtitle}>Completa la información del formulario</Text>
      </View>

      <View style={styles.formContainer}>
        <FormularioScreen form={form} handleChange={handleChange} />
      </View>

      <View style={styles.buttonContainer}>
        <TouchableOpacity 
          style={styles.cancelButton} 
          onPress={() => navigation.goBack()}
        >
          <Text style={styles.cancelButtonText}>Cancelar</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.saveButton} onPress={registerForm}>
          <Text style={styles.saveButtonText}>Guardar Formulario</Text>
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

export default FormRegisterScreen;