import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { SHARED_MATERIAL_PROVIDERS } from '../shared/MaterialImports/shared.materials';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    MatSidenavModule,
    SHARED_MATERIAL_PROVIDERS,
    RouterModule
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

}
