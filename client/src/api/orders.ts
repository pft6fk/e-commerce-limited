import { Order } from "../types";

const API_BASE = "http://localhost:5265/api";

export async function getOrder(id: string): Promise<Order> {
    const response = await fetch(`${API_BASE}/Orders/${id}`);
    return response.json();
}

export async function getOrdersByCustomer(customerId: string): Promise<Order[]> {
    const response = await fetch(`${API_BASE}/Orders/by-customer/${customerId}`);
    return response.json();
}

export async function createOrder(customerId: string): Promise<string> {
    const response = await fetch(`${API_BASE}/Orders`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ customerId }),
    });
    return response.json();
}

export async function addOrderItem(orderId: string, item: {
    productId: string;
    quantity: number;
    unitPrice: number;
    currency: string;
}): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${orderId}/items`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
    });
    return response.status;
}

export async function removeOrderItem(orderId: string, itemId: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${orderId}/items/${itemId}`, {
        method: "DELETE",
    });
    return response.status;
}

export async function payOrder(id: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${id}/pay`, { method: "PUT" });
    return response.status;
}

export async function shipOrder(id: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${id}/ship`, { method: "PUT" });
    return response.status;
}

export async function completeOrder(id: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${id}/complete`, { method: "PUT" });
    return response.status;
}

export async function cancelOrder(id: string): Promise<number> {
    const response = await fetch(`${API_BASE}/Orders/${id}/cancel`, { method: "PUT" });
    return response.status;
}
