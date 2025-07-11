import React, { useEffect, useState } from "react";
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert, Switch } from "react-native";
import { RouteProp, useRoute, useNavigation } from "@react-navigation/native";
import { FormtackParamsList } from "../../navigations/types";
import { IForm } from "../../api/types/IForm";
import { getByIdEntity, updateEntity } from "../../api/apiForm";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import FormularioScreen from "../../Components/FormularioScreen";

type DetailsRouteProp = RouteProp<FormtackParamsList, "FormUpdate">;
type NavigationProp = NativeStackNavigationProp<FormtackParamsList>;

export default function FormUpdateScreen() {
  const [form, setForm] = useState<IForm>({
    id: 0,
    url: "",
    name: "",
    description: "",
    isdeleted: false
  });

  const route = useRoute<DetailsRouteProp>();
  const navigation = useNavigation<NavigationProp>();
  const { id } = route.params;
  const [originalForm, setOriginalForm] = useState<IForm | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getByIdEntity<IForm>(Number(id), "Form");
        setForm(response);
        setOriginalForm(response);  // Guardas copia original
      } catch (error) {
        Alert.alert("Error", "No se pudo obtener el formulario.");
      }
    };

    fetchData();
  }, []);

  const handleChange = (name: string, value: string | boolean) => {
    setForm({ ...form, [name]: value });
  };

  const requestUpdateForm = async () => {
    if (!originalForm) return;

    const hasChanges =
      form.name.trim() !== originalForm.name.trim() ||
      form.description.trim() !== originalForm.description.trim() ||
      form.isdeleted !== originalForm.isdeleted;

    if (!hasChanges) {
      Alert.alert("Sin cambios", "Debes realizar al menos un cambio real para actualizar.");
      return;
    }

    try {
      await updateEntity<IForm>(form, "Form");
      Alert.alert("Éxito", "Formulario actualizado correctamente.", [
        { text: "OK", onPress: () => navigation.goBack() },
      ]);
    } catch (error) {
      console.error("Error completo:", error);
      Alert.alert("Error", "Hubo un problema al actualizar el formulario.");
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Editar Formulario</Text>
        <Text style={styles.subtitle}>Modifica la información del formulario</Text>
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

        <TouchableOpacity style={styles.saveButton} onPress={requestUpdateForm}>
          <Text style={styles.saveButtonText}>Guardar Cambios</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

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