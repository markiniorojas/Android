export class ApiService<T> {
  private readonly endpoint: string;

  constructor(endpoint: string) {
    this.endpoint = endpoint;
  }

  async create(register: T): Promise<T | Error> {
    try {
      const response = await fetch(this.endpoint, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(register),
      });
      if (!response.ok) throw new Error("Error al crear");
      return await response.json();
    } catch (error) {
      return error as Error;
    }
  }

  async getAll(): Promise<T[] | Error> {
    try {
      const response = await fetch(this.endpoint);
      if (!response.ok) throw new Error("Error al listar");
      return await response.json();
    } catch (error) {
      return error as Error;
    }
  }

  async getById(id: number | string): Promise<T | Error> {
    try {
      const response = await fetch(`${this.endpoint}${id}`);
      if (!response.ok) throw new Error("Error al obtener por ID");
      return await response.json();
    } catch (error) {
      return error as Error;
    }
  }

  async update(id: number | string, register: T): Promise<T | Error> {
    try {
      const response = await fetch(`${this.endpoint}${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(register),
      });
      if (!response.ok) throw new Error("Error al actualizar");
      return await response.json();
    } catch (error) {
      return error as Error;
    }
  }

  async remove(id: number | string): Promise<T | Error> {
    try {
      const response = await fetch(`${this.endpoint}${id}`, {
        method: "DELETE",
      });
      if (!response.ok) throw new Error("Error al eliminar");
      return await response.json();
    } catch (error) {
      return error as Error;
    }
  }
}
