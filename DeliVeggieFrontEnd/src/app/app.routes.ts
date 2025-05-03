import { Routes } from '@angular/router';
import { ProductDetailsComponent } from './Components/product-details/product-details.component';
import { ProductListingComponent } from './Components/product-listing/product-listing.component';

export const routes: Routes = [
    {path:'',redirectTo:'products',pathMatch:'full'},
    {path:'products',component:ProductListingComponent},
    {path:'product/:id',component:ProductDetailsComponent}
];
