import React from "react";
import { ScrollView, StyleSheet, Switch, Text, TextInput, View } from "react-native";
import { IModule } from "../api/types/IModule";

interface Props {
    Module: IModule;
    handleChange: (field: keyof IModule, value: string | boolean) => void;
}

const ModuleScreen: React.FC<Props> = ({ Module, handleChange }) => {
    return (
        <ScrollView contentContainerStyle={styles.container}>
            <Text style={styles.label}>Nombre</Text>
            <TextInput
                style={styles.input}
                placeholder="Ingrese el nombre"
                value={Module.name}
                onChangeText={(text) => handleChange("name", text)}
            />

            <Text style={styles.label}>Descripción</Text>
            <TextInput
                style={[styles.input, styles.textArea]}
                placeholder="Ingrese la descripción"
                multiline
                numberOfLines={4}
                value={Module.description}
                onChangeText={(text) => handleChange("description", text)}
            />

            <View style={styles.switchContainer}>
                <Text style={styles.label}>Activo</Text>
                <Switch
                    value={Module.isdeleted}
                    onValueChange={(value) => handleChange("isdeleted", value)}
                    thumbColor={Module.isdeleted ? "#4CAF50" : "#f4f3f4"}
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

export default ModuleScreen;
