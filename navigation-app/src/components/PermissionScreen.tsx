// components/PermissionScreen.tsx
import React from "react";
import { View, TextInput, ScrollView, StyleSheet, Switch, Text } from "react-native";
import { IForm } from "../api/types/IForm";

interface Props {
  form: IForm;
  handleChange: (field: keyof IForm, value: string | boolean) => void;
}

const FormForm: React.FC<Props> = ({ form, handleChange }) => {
  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TextInput
        style={styles.input}
        placeholder="Nombre"
        value={form.name}
        onChangeText={(text) => handleChange("name", text)}
      />
      <TextInput
        style={styles.input}
        placeholder="Descripción"
        value={form.description}
        onChangeText={(text) => handleChange("description", text)}
        multiline
        numberOfLines={3}
      />
     <View style={styles.switchRow}>
        <Text style={styles.label}>¿Eliminado?</Text>
        <Switch
          value={form.isDeleted}
          onValueChange={(value) => handleChange("isDeleted", value)}
        />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 20,
  },
  input: {
    borderWidth: 1,
    borderColor: "#ccc",
    padding: 12,
    borderRadius: 8,
    marginBottom: 16,
  },
  switchRow: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    marginBottom: 16,
  },
  label: {
    fontSize: 16,
    fontWeight: "500",
  },
});

export default FormForm;
