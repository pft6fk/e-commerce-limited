export interface Product {
    id: string;
    name: string;
    description: string;
    price: number;
    currency: string;
    isActive: boolean;
}

export interface Customer {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    registeredAt: string;
}

export interface Order {
    id: string;
    customerId: string;
    status: string;
    totalPrice: number;
    currency: string;
    createdAt: string;
    items: OrderItem[];
}

export interface OrderItem {
    id: string;
    productId: string;
    productName: string;
    quantity: number;
    unitPrice: number;
    currency: string;
}

export interface Payment {
    id: string;
    orderId: string;
    amount: number;
    currency: string;
    status: string;
    processedAt: string;
}

export interface StockItem {
    productId: string;
    availableQuantity: number;
}