import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private _http:HttpClient;
  private productGatewayUrl="http://localhost:5264";
  constructor(httpRef: HttpClient) { 
    this._http= httpRef;
  }

  getProducts(): Observable<any>{
   return this._http.get(this.productGatewayUrl+"/products");
  }

  getProductDetails(id:number){
    return this._http.get(this.productGatewayUrl+"/products/"+id).pipe(tap(p=>console.log(p)));
  }
}
