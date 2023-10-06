export interface Boxfeed {
    boxId: number
    name: string
    size: string
    price: number
    boxImgUrl: string
}

export interface Box {
    boxId: number
    name: string
    size: string
    description: string
    price: number
    boxImgUrl: string
}

export interface OrderFeed {
    orderId: number
    customerId: number
    price: number
}

export interface Order {
  orderId: number
  customerId: number
  price: number
  orders: Orders[]
}

export interface Orders {
  amount: number
  boxId: number
}

