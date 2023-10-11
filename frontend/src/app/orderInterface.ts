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

export interface cartItem
{
  name: string
  boxId: number
  price: number
  amount: number
  boximgurl: string
}

export interface customer
{
  customerId: number
  name: string
  mail: string
  tlf: string
  address: string
}
