import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-view-toggle',
  imports: [
    CommonModule
  ],
  templateUrl: './view-toggle.component.html',
  styleUrl: './view-toggle.component.scss'
})
export class ViewToggleComponent {
  @Input() viewMode: 'available' | 'rented' = 'available';
  @Output() modeChanged = new EventEmitter<'available' | 'rented'>();
}