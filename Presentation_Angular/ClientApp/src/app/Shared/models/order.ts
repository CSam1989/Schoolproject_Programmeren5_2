import { Customer } from "./customer";
import { ShoppingCartItem } from "./shoppingCartItem";

export interface Order {
  id?: number;
  isPayed?: boolean;
  orderDate?: Date;
  streetShipping: string;
  houseNrShipping: string;
  houseBusShipping: string;
  postalcodeShipping: string;
  cityShipping: string;
  customerId: number;
  customer?: Customer;
  orderLines: ShoppingCartItem[];
  orderSummary?: {
    totalCount: number;
    totalExVat: number;
    vat: number;
    totalInclVat: number;
  };
}
