import { useEffect, useState } from "react";
import { Product } from "../types";
import { getProducts }  from "../api/products";

function ProductsPage(){
    const [products, setProducts] = useState<Product[]>([]);
    
    useEffect(() => {
        getProducts().then(data => setProducts(data));
    }, []);

    return (
        <div>
            <h1>Products</h1>
            {products.map(product => (
                <div key={product.id}> 
                    <h3>{product.name}</h3>
                    <p>{product.description}</p>
                    <p>{product.price} {product.currency}</p>
                    <p>{product.isActive ? "Active" : "Inactive"}</p>
                </div>
            ))}
        </div>
    );
}

export default ProductsPage;
