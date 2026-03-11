import { Product } from "../types";

const API_BASE = "http://localhost:5265/api";

export async function getProducts(): Promise<Product[]> {
    const response = await fetch(`${API_BASE}/Products`);
    return response.json();
}

export async function getProduct(id: string): Promise<Product> {
    const response = await fetch(`${API_BASE}/Products/${id}`);
    return response.json();
}

export async function createProduct(product : {
  name: string;
  description: string;
  price: number;
  currency: string;
  isActive: boolean;
}) : Promise<string>{
    const response = await fetch(`${API_BASE}/Products`, {
      method: 'POST',
      body: JSON.stringify(product),
      headers: { "Content-Type": "application/json" }
    });
    return response.json();
}

///returns status code
export async function updateProductPrice(id: string, productPrice : {
  price: number;
  currency: string;
}) : Promise<number>{
    const response = await fetch(`${API_BASE}/Products/${id}/price`, {
      method: 'PUT',
      body: JSON.stringify(productPrice),
      headers: { "Content-Type": "application/json" }
    });
    return response.status;
}

///returns status code
export async function activateProduct(id: string) : Promise<number>{
    const response = await fetch(`${API_BASE}/Products/${id}/activate`, { method: 'PUT' });
    return response.status;
}

///returns status code
export async function deactivateProduct(id: string) : Promise<number>{
    const response = await fetch(`${API_BASE}/Products/${id}/deactivate`, { method: 'PUT' });
    return response.status;
}

