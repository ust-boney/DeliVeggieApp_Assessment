import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-details',
  imports: [CurrencyPipe],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
   private productService:ProductService;
   product:any;

constructor(productServRef:ProductService, private route:ActivatedRoute){
  this.productService= productServRef;
}
  ngOnInit(): void {
   
    const id:number= Number(this.route.snapshot.paramMap.get('id')) || 0;
    this.productService.getProductDetails(id).subscribe(resp=>{
      this.product=resp;
    });
  }

}
