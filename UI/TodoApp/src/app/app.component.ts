import { Component } from '@angular/core';
import { NavComponent } from './components/nav/nav.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'TodoApp';
}
