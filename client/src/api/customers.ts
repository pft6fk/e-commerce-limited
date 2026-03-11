import { Customer } from "../types";

const API_BASE = "http://localhost:5265/api";

export async function getCustomer(id:string): Promise<Customer>{
    const response = await fetch(`${API_BASE}/Customers/${id}`);
    return response.json();
}

export async function registerCustomer(
    customer : {   
        firstName: string; 
        lastName: string;
        email: string;
    }): Promise<string>{
        const response = await fetch(`${API_BASE}/Customers`, { 
            method: 'POST', body: JSON.stringify(customer),
            headers: { "Content-Type": "application/json" }
        });
        return response.json();
}