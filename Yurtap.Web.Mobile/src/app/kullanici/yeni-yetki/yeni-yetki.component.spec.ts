import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { YeniYetkiComponent } from './yeni-yetki.component';

describe('OgrenciYetkiComponent', () => {
  let component: YeniYetkiComponent;
  let fixture: ComponentFixture<YeniYetkiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YeniYetkiComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YeniYetkiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
