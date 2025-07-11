import React from "react";
import { ScrollView, StyleSheet, Switch, Text, TextInput, View } from "react-native";
import { IForm } from "../api/types/IForm";

interface Props {
  form: IForm;
  handleChange: (field: keyof IForm, value: string | boolean) => void;
}

const FormularioScreen: React.FC<Props> = ({ form, handleChange }) => {
  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.label}>Nombre</Text>
      <TextInput
        style={styles.input}
        placeholder="Ingrese el nombre"
        value={form.name}
        onChangeText={(text) => handleChange("name", text)}
      />

      <Text style={styles.label}>Descripción</Text>
      <TextInput
        style={[styles.input, styles.textArea]}
        placeholder="Ingrese la descripción"
        multiline
        numberOfLines={4}
        value={form.description}
        onChangeText={(text) => handleChange("description", text)}
      />

      <TextInput
        style={[styles.input, styles.textArea]}
        placeholder="Ingrese la direccion URL"
        multiline
        numberOfLines={4}
        value={form.description}
        onChangeText={(text) => handleChange("url", text)}
      />

      <View style={styles.switchContainer}>
        <Text style={styles.label}>Activo</Text>
        <Switch
          value={form.isdeleted}
          onValueChange={(value) => handleChange("isdeleted", value)}
          thumbColor={form.isdeleted ? "#4CAF50" : "#f4f3f4"}
          trackColor={{ false: "#ccc", true: "#81C784" }}
        />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 20,
    backgroundColor: "#f2f2f2",
    borderRadius: 12,
    margin: 10,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 3,
  },
  label: {
    fontSize: 16,
    color: "#333",
    marginBottom: 8,
    fontWeight: "bold",
  },
  input: {
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: "#ddd",
    padding: 12,
    borderRadius: 8,
    marginBottom: 16,
    fontSize: 15,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
  },
  textArea: {
    height: 100,
    textAlignVertical: "top",
  },
  switchContainer: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    marginBottom: 16,
    paddingVertical: 10,
  },
});

export default FormularioScreen;
