// screens/Form/FormListScreen.tsx
import React, { useEffect, useState } from "react";
import { View, Text, FlatList, TouchableOpacity, StyleSheet } from "react-native";
import { IForm } from "../../api/types/IForm";
import { useNavigation } from "@react-navigation/native";

export default function FormListScreen() {
  const [forms, setForms] = useState<IForm[]>([]);
  const navigation = useNavigation<any>();

  useEffect(() => {
    loadForms();
  }, []);

  const loadForms = async () => {
    const result = await formService.getAll(); // <-- USANDO TU MÉTODO GENÉRICO
    if (!(result instanceof Error)) setForms(result);
    else console.error(result.message);
  };

  const renderItem = ({ item }: { item: IForm }) => (
    <TouchableOpacity
      style={styles.card}
      onPress={() => navigation.navigate("FormUpdate", { id: item.id })}
    >
      <Text style={styles.title}>{item.name}</Text>
      <Text>{item.description}</Text>
    </TouchableOpacity>
  );

  return (
    <View style={styles.container}>
      <TouchableOpacity
        style={styles.addButton}
        onPress={() => navigation.navigate("FormRegister")}
      >
        <Text style={styles.addText}>+ Agregar Formulario</Text>
      </TouchableOpacity>

      <FlatList
        data={forms}
        keyExtractor={(item) => item.id}
        renderItem={renderItem}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 16 },
  addButton: {
    backgroundColor: "#1e90ff",
    padding: 12,
    borderRadius: 8,
    marginBottom: 16,
    alignItems: "center",
  },
  addText: {
    color: "#fff",
    fontWeight: "bold",
  },
  card: {
    backgroundColor: "#f9f9f9",
    padding: 16,
    borderRadius: 8,
    marginBottom: 12,
    elevation: 2,
  },
  title: {
    fontWeight: "bold",
    fontSize: 16,
    marginBottom: 4,
  },
});
