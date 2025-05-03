import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProductListingComponent } from './Components/product-listing/product-listing.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DeliVeggieFrontEnd';
}
