import { MODULE_END_POINT} from "../constants/enpoints";
import { IModule } from "./types/IModule"

export  const createModule = async (register: IModule)  => {
    try {
    const response = await fetch(MODULE_END_POINT, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(register),
    });

    if (!response.ok) throw new Error("Error al crear el Módulo");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
 };

export const getAllModule = async () => {
        try {
            const response = await fetch(MODULE_END_POINT);
            if (!response.ok) throw new Error("Error al listar los Módulo");
            let data = await response.json();
            console.log(data);
            return data;
        } catch (error) {
            return error;
        }
};

export const getByIdModule = async (id: string) => {
  try {
    const response = await fetch(MODULE_END_POINT + id);

    if (!response.ok) throw new Error("Error al actualizar el Módulo");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
};

export const updateModule = async (id: String, register: IModule) => {
  try {
    const response = await fetch(MODULE_END_POINT + id, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(register),
    });

    if (!response.ok) throw new Error("Error al actualizar el Módulo");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
};

export const deleteModule = async (id: string) => {
  try {
    const response = await fetch(MODULE_END_POINT + id, {
      method: "DELETE",
    });

    if (!response.ok) throw new Error("Error al eliminar el libro");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
};
