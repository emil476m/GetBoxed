export interface OrderFeed {
  orderId: number
  customerId: number
  price: number
}

export interface Order {
  orderId?: number
  customerId?: number
  totalPrice?: number
  orderDate?: string
  boxOrder?: Orders[]
}

export interface Orders {
  amount: number
  boxId: number
}
