import React, { useEffect, useState } from "react";
import { View, Text, Alert, StyleSheet, TouchableOpacity } from "react-native";
import { RouteProp, useRoute, useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { PermissionTasckParamsList } from "../../navigations/types";
import { deleteEntity, getByIdEntity } from "../../api/apiForm";
import { IPermission } from "../../api/types/IPermission";

type DetailsRouteProp = RouteProp<PermissionTasckParamsList, "PermissionDelete">;
type NavigationProp = NativeStackNavigationProp<PermissionTasckParamsList>;

export default function PermissionDeleteScreen() {
  const [Permission, setPermission] = useState<IPermission | null>(null);

  const route = useRoute<DetailsRouteProp>();
  const navigation = useNavigation<NavigationProp>();
  const { id } = route.params;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getByIdEntity<IPermission>(Number(id), "Permission");
        setPermission(response);
      } catch (error) {
        Alert.alert("Error", "No se pudo obtener el permiso.");
      }
    };

    fetchData();
  }, []);

  const handleDelete = async () => {
    Alert.alert(
      "Confirmación",
      "¿Estás seguro de eliminar este permiso?",
      [
        { text: "Cancelar", style: "cancel" },
        {
          text: "Eliminar",
          style: "destructive",
          onPress: async () => {
            try {
              await deleteEntity(Number(id), "Permission", "Logical");
              Alert.alert("Éxito", "permiso eliminado correctamente.", [
                { text: "OK", onPress: () => navigation.goBack() },
              ]);
            } catch (error) {
              Alert.alert("Error", "Hubo un problema al eliminar el permiso.");
            }
          },
        },
      ]
    );
  };

  if (!Permission) return null;

   return (
        <View style={styles.container}>
          <View style={styles.header}>
            <Text style={styles.title}>Eliminar Permiso</Text>
            <Text style={styles.subtitle}>Esta acción no se puede deshacer</Text>
          </View>
    
          <View style={styles.card}>
            <View style={styles.formInfo}>
              <View style={styles.field}>
                <Text style={styles.label}>Nombre del Permiso</Text>
                <Text style={styles.value}>{Permission.name}</Text>
              </View>
    
              <View style={styles.field}>
                <Text style={styles.label}>Descripción</Text>
                <Text style={styles.value}>{Permission.description || "Sin descripción"}</Text>
              </View>
            </View>
          </View>
    
          <View style={styles.buttonContainer}>
            <TouchableOpacity style={styles.cancelButton} onPress={() => navigation.goBack()}>
              <Text style={styles.cancelButtonText}>Cancelar</Text>
            </TouchableOpacity>
            
            <TouchableOpacity style={styles.deleteButton} onPress={handleDelete}>
              <Text style={styles.deleteButtonText}>Eliminar</Text>
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
      },
      title: {
        fontSize: 24,
        fontWeight: "600",
        color: "#2d3748",
        marginBottom: 4,
      },
      subtitle: {
        fontSize: 14,
        color: "#718096",
      },
      card: {
        backgroundColor: "#ffffff",
        borderRadius: 12,
        padding: 20,
        marginBottom: 32,
        shadowColor: "#000",
        shadowOffset: {
          width: 0,
          height: 2,
        },
        shadowOpacity: 0.05,
        shadowRadius: 8,
        elevation: 2,
      },
      formInfo: {
        gap: 16,
      },
      field: {
        borderBottomWidth: 1,
        borderBottomColor: "#e2e8f0",
        paddingBottom: 12,
      },
      label: {
        fontSize: 12,
        fontWeight: "500",
        color: "#718096",
        textTransform: "uppercase",
        letterSpacing: 0.5,
        marginBottom: 4,
      },
      value: {
        fontSize: 16,
        color: "#2d3748",
        lineHeight: 22,
      },
      buttonContainer: {
        flexDirection: "row",
        gap: 12,
        marginTop: "auto",
        paddingTop: 20,
      },
      cancelButton: {
        flex: 1,
        backgroundColor: "#e2e8f0",
        paddingVertical: 14,
        borderRadius: 8,
        alignItems: "center",
      },
      cancelButtonText: {
        color: "#4a5568",
        fontSize: 16,
        fontWeight: "600",
      },
      deleteButton: {
        flex: 1,
        backgroundColor: "#fed7d7",
        paddingVertical: 14,
        borderRadius: 8,
        alignItems: "center",
        borderWidth: 1,
        borderColor: "#fc8181",
      },
      deleteButtonText: {
        color: "#c53030",
        fontSize: 16,
        fontWeight: "600",
      },
    });
