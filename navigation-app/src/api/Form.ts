import { FORM_END_POINT } from "../constants/enpoints";
import { IForm } from "./types/IForm"

export  const createForm = async (register: IForm)  => {
    try {
    const response = await fetch(FORM_END_POINT, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(register),
    });

    if (!response.ok) throw new Error("Error al crear el formulario");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
 };

export const getAllForm = async () => {
        try {
            const response = await fetch(FORM_END_POINT);
            if (!response.ok) throw new Error("Error al listar los formulario");
            let data = await response.json();
            console.log(data);
            return data;
        } catch (error) {
            return error;
        }
};

export const getByIdForm = async (id: number) => {
  try {
    const response = await fetch(FORM_END_POINT + id);

    if (!response.ok) throw new Error("Error al actualizar el libro");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
};

export const updateForm = async (id: number, register: IForm) => {
  try {
    const response = await fetch(FORM_END_POINT + id, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(register),
    });

    if (!response.ok) throw new Error("Error al actualizar el libro");
    let data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    return error;
  }
};

export const deleteForm = async (id: string) => {
  try {
    const response = await fetch(FORM_END_POINT + id, {
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
