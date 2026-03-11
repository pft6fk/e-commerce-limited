import { Payment } from "../types";

const API_BASE = "http://localhost:5265/api";

export async function getPaymentByOrder(orderId: string): Promise<Payment> {
    const response = await fetch(`${API_BASE}/Payments/by-order/${orderId}`);
    return response.json();
}

export async function createPayment(payment: {
    orderId: string;
    amount: number;
    currency: string;
}): Promise<string> {
    const response = await fetch(`${API_BASE}/Payments`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payment),
    });
    return response.json();
}

export async function processPayment(id: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Payments/${id}/process`, { method: "PUT" });
    return response.status;
}
