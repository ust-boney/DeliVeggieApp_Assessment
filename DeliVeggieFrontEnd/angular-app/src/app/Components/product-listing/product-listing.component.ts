import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-listing',
  imports: [NgFor,RouterLink],
  templateUrl: './product-listing.component.html',
  styleUrl: './product-listing.component.css'
})
export class ProductListingComponent implements OnInit {
  private productService:ProductService;
 constructor(productServRef:ProductService){
  this.productService=productServRef;
 }
  ngOnInit(): void {
   this.getProducts();
  }
  productData: any;
 getProducts(){
   this.productService.getProducts().subscribe(resp=>{
      console.log(resp);
      this.productData=resp;
   })
 }
}
