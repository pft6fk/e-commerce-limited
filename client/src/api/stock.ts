import { StockItem } from "../types";

const API_BASE = "http://localhost:5265/api";

export async function getStockByProduct(productId: string): Promise<StockItem> {
    const response = await fetch(`${API_BASE}/Stock/by-product/${productId}`);
    return response.json();
}

export async function createStock(stock: {
    productId: string;
    quantity: number;
}): Promise<string> {
    const response = await fetch(`${API_BASE}/Stock`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(stock),
    });
    return response.json();
}

export async function increaseStock(productId: string, quantity: number): Promise<number> {
    const response = await fetch(`${API_BASE}/Stock/by-product/${productId}/increase`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ quantity }),
    });
    return response.status;
}

export async function reduceStock(productId: string, quantity: number): Promise<number> {
    const response = await fetch(`${API_BASE}/Stock/by-product/${productId}/reduce`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ quantity }),
    });
    return response.status;
}
