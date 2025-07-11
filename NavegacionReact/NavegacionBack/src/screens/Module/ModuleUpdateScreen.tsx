import React, { useEffect, useState } from "react";
import { View, Text, TouchableOpacity, StyleSheet, Alert, Switch } from "react-native";
import { RouteProp, useRoute, useNavigation } from "@react-navigation/native";
import { ModuleTasckParamsList } from "../../navigations/types";
import { getByIdEntity, updateEntity } from "../../api/apiForm";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { IModule } from "../../api/types/IModule";
import ModuleScreen from "../../Components/ModuleScreen";

type DetailsRouteProp = RouteProp<ModuleTasckParamsList, "ModuleUpdate">;
type NavigationProp = NativeStackNavigationProp<ModuleTasckParamsList>;

export default function ModuleUpdateScreen() {
  const [Module, setModule] = useState<IModule>({
    id: 0,
    name: "",
    description: "",  
    isdeleted: false,
  });

  const route = useRoute<DetailsRouteProp>();
  const navigation = useNavigation<NavigationProp>();
  const { id } = route.params;
  const [originalModule, setOriginalModule] = useState<IModule | null>(null);

  useEffect(() => {
  const fetchData = async () => {
    try {
      const response = await getByIdEntity<IModule>(Number(id), "Module");
      setModule(response);
      setOriginalModule(response);  // Guardas copia original
    } catch (error) {
      Alert.alert("Error", "No se pudo obtener el Modulo.");
    }
  };

  fetchData();
}, []);

  const handleChange = (name: string, value: string | boolean) => {
    setModule({ ...Module, [name]: value });
  };

  const requestUpdateModule = async () => {
  if (!originalModule) return;

  const hasChanges =
    Module.name.trim() !== originalModule.name.trim() ||
    Module.description.trim() !== originalModule.description.trim() ||
    Module.isdeleted !== originalModule.isdeleted;

  if (!hasChanges) {
    Alert.alert("Sin cambios", "Debes realizar al menos un cambio real para actualizar.");
    return;
  }

  try {
    await updateEntity<IModule>(Module, "Module");
    Alert.alert("Éxito", "modulo actualizado correctamente.", [
      { text: "OK", onPress: () => navigation.goBack() },
    ]);
  } catch (error) {
    console.error("Error completo:", error);
    Alert.alert("Error", "Hubo un problema al actualizar el Modulo.");
  }
};


  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Editar Modulo</Text>
        <Text style={styles.subtitle}>Modifica la información del Modulo</Text>
      </View>

      <View style={styles.formContainer}>
        <ModuleScreen Module={Module} handleChange={handleChange} />
      </View>

      <View style={styles.buttonContainer}>
        <TouchableOpacity 
          style={styles.cancelButton} 
          onPress={() => navigation.goBack()}
        >
          <Text style={styles.cancelButtonText}>Cancelar</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.saveButton} onPress={requestUpdateModule}>
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