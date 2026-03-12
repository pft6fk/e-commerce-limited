import { useEffect, useState } from "react";
import { Order } from "../types";
import { getOrdersByCustomer } from "../api/orders";
function OrdersPage() {
    const [userId, setUserId] = useState<string>();
    const [orders, setOrders] = useState<Order[]>([]);
    function handleSearch(){
        console.log(userId);
        getOrdersByCustomer(userId).then(data => setOrders(data));
    }
    return (
        <div>
            <div>
                <input value={userId} onChange={e => setUserId(e.target.value)}/>
                <button onClick={handleSearch}>Search</button>
            </div>
            <div>
                <h3>Orders</h3>
                {orders.map(order => (
                    <div key={order.id}>
                            {
                                order.items.map(item => (
                                    <div key={item.id}>
                                        <p>{item.productName}</p>
                                        <p>{item.unitPrice}</p>
                                        <p>{item.currency}</p>
                                        <p>{item.quantity}</p>
                                    </div>
                                ))
                            }
                            <p>{order.status}</p>
                            <p>{order.createdAt}</p>
                            <p>{order.totalPrice}</p>
                            <p>{order.currency}</p>
                        </div>
                        ))}
            </div>
        </div>
    );
}
export default OrdersPage;