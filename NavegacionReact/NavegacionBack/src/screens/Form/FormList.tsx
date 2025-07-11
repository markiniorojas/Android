import React, { useEffect, useState } from "react";
import { View, Text, TouchableOpacity, StyleSheet, ScrollView, Alert } from "react-native";
import { useNavigation, useFocusEffect } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { FormtackParamsList } from "../../navigations/types";
import { IForm } from "../../api/types/IForm";
import { deleteEntity, getAllEntity } from "../../api/apiForm";

type FormScreenNavigationProp = NativeStackNavigationProp<FormtackParamsList, "FormList">;

const FormList = () => {
  const navigation = useNavigation<FormScreenNavigationProp>();
  const [forms, setForms] = useState<IForm[]>([]);

  useFocusEffect(
    React.useCallback(() => {
      fetchForms();
    }, [])
  );

  const fetchForms = async () => {
    try {
      const data = await getAllEntity<IForm>("Form");
      setForms(data);
    } catch (error) {
      console.error("Error al traer los formularios", error);
    }
  };

  const handleDelete = (id: number) => {
    Alert.alert(
      "Confirmación",
      "¿Estás seguro de eliminar este formulario?",
      [
        { text: "Cancelar", style: "cancel" },
        {
          text: "Eliminar",
          style: "destructive",
          onPress: async () => {
            try {
              await deleteEntity(id, "FormControllerPrueba", "Logical");
              Alert.alert("Éxito", "Formulario eliminado correctamente.");
              fetchForms();
            } catch (error) {
              Alert.alert("Error", "Hubo un problema al eliminar el formulario.");
            }
          },
        },
      ]
    );
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Formularios</Text>
        <Text style={styles.headerSubtitle}>Gestiona tus formularios</Text>
      </View>

      <TouchableOpacity style={styles.addButton} onPress={() => navigation.navigate("FormRegister")}>
        <Text style={styles.addButtonText}>+ Agregar Formulario</Text>
      </TouchableOpacity>

      <ScrollView style={styles.listContainer} showsVerticalScrollIndicator={false}>
        {forms.length === 0 ? (
          <View style={styles.emptyState}>
            <Text style={styles.emptyTitle}>No hay formularios</Text>
            <Text style={styles.emptySubtitle}>Crea tu primer formulario para comenzar</Text>
          </View>
        ) : (
          forms.map((item) => (
            <View key={item.id} style={styles.card}>
              <View style={styles.cardHeader}>
                <View style={styles.titleContainer}>
                  <Text style={styles.title}>{item.name}</Text>
                  <View style={styles.statusBadge}>
                    <Text style={[styles.status, item.isdeleted ? styles.deleted : styles.active]}>
                      {item.isdeleted ? "Eliminado" : "Activo"}
                    </Text>
                  </View>
                </View>
              </View>

              <Text style={styles.description}>
                {item.description || "Sin descripción disponible"}
              </Text>

              <View style={styles.divider} />

              <View style={styles.buttonRow}>
                <TouchableOpacity
                  style={styles.updateButton}
                  onPress={() => navigation.navigate("FormUpdate", { id: item.id.toString() })}
                >
                  <Text style={styles.updateButtonText}>Actualizar</Text>
                </TouchableOpacity>
                
                <TouchableOpacity
                  style={styles.deleteButton}
                  onPress={() => handleDelete(item.id)}
                >
                  <Text style={styles.deleteButtonText}>Eliminar</Text>
                </TouchableOpacity>
              </View>
            </View>
          ))
        )}
      </ScrollView>
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
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: "700",
    color: "#2d3748",
    marginBottom: 4,
  },
  headerSubtitle: {
    fontSize: 16,
    color: "#718096",
  },
  addButton: {
    backgroundColor: "#4a5568",
    paddingVertical: 14,
    paddingHorizontal: 20,
    borderRadius: 10,
    alignItems: "center",
    marginBottom: 20,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  addButtonText: {
    color: "#ffffff",
    fontWeight: "600",
    fontSize: 16,
    letterSpacing: 0.5,
  },
  listContainer: {
    flex: 1,
  },
  emptyState: {
    alignItems: "center",
    justifyContent: "center",
    paddingVertical: 60,
  },
  emptyTitle: {
    fontSize: 20,
    fontWeight: "600",
    color: "#4a5568",
    marginBottom: 8,
  },
  emptySubtitle: {
    fontSize: 14,
    color: "#718096",
    textAlign: "center",
  },
  card: {
    backgroundColor: "#ffffff",
    padding: 20,
    marginBottom: 16,
    borderRadius: 12,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 8,
    elevation: 2,
    borderWidth: 1,
    borderColor: "#e2e8f0",
  },
  cardHeader: {
    marginBottom: 12,
  },
  titleContainer: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "flex-start",
  },
  title: {
    fontSize: 18,
    fontWeight: "600",
    color: "#2d3748",
    flex: 1,
    marginRight: 12,
  },
  statusBadge: {
    alignSelf: "flex-start",
  },
  status: {
    fontSize: 11,
    fontWeight: "600",
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderRadius: 12,
    textTransform: "uppercase",
    letterSpacing: 0.5,
  },
  active: {
    backgroundColor: "#c6f6d5",
    color: "#22543d",
  },
  deleted: {
    backgroundColor: "#fed7d7",
    color: "#c53030",
  },
  description: {
    fontSize: 14,
    color: "#718096",
    lineHeight: 20,
    marginBottom: 16,
  },
  divider: {
    height: 1,
    backgroundColor: "#e2e8f0",
    marginBottom: 16,
  },
  buttonRow: {
    flexDirection: "row",
    gap: 12,
  },
  updateButton: {
    flex: 1,
    backgroundColor: "#edf2f7",
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
    alignItems: "center",
    borderWidth: 1,
    borderColor: "#cbd5e0",
  },
  updateButtonText: {
    color: "#4a5568",
    fontWeight: "600",
    fontSize: 14,
  },
  deleteButton: {
    flex: 1,
    backgroundColor: "#fed7d7",
    paddingVertical: 10,
    paddingHorizontal: 16,
    borderRadius: 8,
    alignItems: "center",
    borderWidth: 1,
    borderColor: "#fc8181",
  },
  deleteButtonText: {
    color: "#c53030",
    fontWeight: "600",
    fontSize: 14,
  },
});

export default FormList;